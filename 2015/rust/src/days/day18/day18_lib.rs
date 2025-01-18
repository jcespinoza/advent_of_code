pub fn animate_lights(starting_grid: Vec<Vec<i32>>, steps: i32) -> usize {
  let mut grid = starting_grid.clone();
  for _ in 0..steps {
    let mut new_grid = vec![];
    for row in 0..grid.len() {
      let mut new_row = vec![];
      for col in 0..grid[row].len() {
        let neighbor_count = get_neighbor_count(&grid, row, col);
        let current_state = grid[row][col];
        // Match on the two values at once
        let new_value = match (current_state, neighbor_count) {
          (1, 2) | (1, 3) | (0, 3) => 1,
          _ => 0,
        };
        new_row.push(new_value);
      }
      new_grid.push(new_row);
    }
    grid = new_grid;
  }

  let on_lights = grid.iter().flatten().filter(|&&x| x == 1).count();
  on_lights
}

fn get_neighbor_count(grid: &Vec<Vec<i32>>, y: usize, x: usize) -> i32 {
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
