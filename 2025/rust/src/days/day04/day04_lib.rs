pub struct PaperWarehouse {
  pub grid: Vec<Vec<SlotContent>>, // 2D grid representing the paper rolls
}

pub enum SlotContent {
  Empty, // Represented by a dot '.'
  Roll,  // Represented by an '@' symbol
}

impl From<&str> for SlotContent {
  fn from(s: &str) -> Self {
    match s {
      "." => SlotContent::Empty,
      "@" => SlotContent::Roll,
      _ => panic!("Invalid slot content: {}", s),
    }
  }
}

impl PaperWarehouse {
  pub fn from_lines(lines: Vec<&str>) -> Self {
    let grid: Vec<Vec<SlotContent>> = lines
      .iter()
      .map(|line| {
        line
          .chars()
          .map(|c| SlotContent::from(&c.to_string()[..]))
          .collect()
      })
      .collect();
    PaperWarehouse { grid }
  }

  // Finds it the item at the given row and column is accessible. The forklifts can only access a roll of paper if there are fewer than four rolls of paper in the eight adjacent positions.
  pub fn is_accessible(&self, row: usize, col: usize) -> bool {
    let directions = vec![
      (-1, -1),
      (-1, 0),
      (-1, 1),
      (0, -1),
      (0, 1),
      (1, -1),
      (1, 0),
      (1, 1),
    ];

    let mut adjacent_rolls = 0;

    for (dr, dc) in directions {
      let new_row = row as isize + dr;
      let new_col = col as isize + dc;

      if new_row >= 0
        && new_row < self.grid.len() as isize
        && new_col >= 0
        && new_col < self.grid[0].len() as isize
      {
        if let SlotContent::Roll = self.grid[new_row as usize][new_col as usize] {
          adjacent_rolls += 1;
        }
      }

      // Early exit if we already have 4 or more adjacent rolls
      if adjacent_rolls >= 4 {
        return false;
      }
    }

    adjacent_rolls < 4
  }

  pub fn count_accessible_rolls(&self) -> i64 {
    let mut accessible_count = 0;

    for (row_idx, row) in self.grid.iter().enumerate() {
      for (col_idx, slot) in row.iter().enumerate() {
        if let SlotContent::Roll = slot {
          if self.is_accessible(row_idx, col_idx) {
            accessible_count += 1;
          }
        }
      }
    }

    accessible_count
  }
}
