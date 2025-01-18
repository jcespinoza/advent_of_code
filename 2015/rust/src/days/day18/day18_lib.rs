pub fn animate_lights(starting_grid: Vec<Vec<i32>>, steps: i32, corners_always_on: bool) -> usize {
  let mut grid = starting_grid.clone();
  for _ in 0..steps {
    let mut new_grid = vec![];
    for row in 0..grid.len() {
      let mut new_row = vec![];
      for col in 0..grid[row].len() {
        let neighbor_count = get_neighbor_count(&grid, row, col);
        let current_state = grid[row][col];

        let new_value = compute_new_value(
          corners_always_on,
          &grid,
          row,
          col,
          neighbor_count,
          current_state,
        );

        new_row.push(new_value);
      }
      new_grid.push(new_row);
    }
    grid = new_grid;
  }

  let on_lights = grid.iter().flatten().filter(|x| **x == 1).count();
  on_lights
}

fn compute_new_value(
  corners_always_on: bool,
  grid: &[Vec<i32>],
  row: usize,
  col: usize,
  neighbor_count: i32,
  current_state: i32,
) -> i32 {
  let pair = (row, col);
  let is_corner = pair == (0, 0)
    || pair == (0, grid[row].len() - 1)
    || pair == (grid.len() - 1, 0)
    || pair == (grid.len() - 1, grid[row].len() - 1);

  // Match on the two values at once
  let mut new_value = match (current_state, neighbor_count) {
    (1, 2) | (1, 3) | (0, 3) => 1,
    _ => 0,
  };

  if is_corner && corners_always_on {
    new_value = 1;
  }

  new_value
}

fn get_neighbor_count(grid: &[Vec<i32>], y: usize, x: usize) -> i32 {
  let mut neighbors = 0;
  for dy in -1..=1 {
    for dx in -1..=1 {
      if dy == 0 && dx == 0 {
        continue;
      }

      let ny = y as i32 + dy;
      let nx = x as i32 + dx;
      if ny < 0 || ny >= grid.len() as i32 || nx < 0 || nx >= grid[y].len() as i32 {
        continue;
      }

      neighbors += grid[ny as usize][nx as usize];
    }
  }
  neighbors
}
