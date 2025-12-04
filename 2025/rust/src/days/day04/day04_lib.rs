#[derive(Clone, Debug)]
pub struct PaperWarehouse {
  pub grid: Vec<Vec<SlotContent>>, // 2D grid representing the paper rolls
}

#[derive(Clone, Copy, Debug, PartialEq, Eq)]
pub enum SlotContent {
  Empty,   // Represented by a dot '.'
  Roll,    // Represented by an '@' symbol
  Removed, // Represented by an 'x' symbol
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

  pub fn count_removed_rolls(&self) -> i64 {
    let mut removed_count = 0;

    for row in &self.grid {
      for slot in row {
        if let SlotContent::Removed = slot {
          removed_count += 1;
        }
      }
    }

    removed_count
  }

  // Traverse the warehouse and repeatedly remove all accessible rolls of paper until no more can be accessed. Mark removed rolls with 'x' (using the enum).
  #[deprecated(
    note = "Use `remove_accessible_rolls_queue` for better performance (avoids full-grid rescans)"
  )]
  pub fn remove_accessible_rolls(&mut self) {
    loop {
      let mut was_any_roll_removed = false;

      for row_idx in 0..self.grid.len() {
        for col_idx in 0..self.grid[0].len() {
          if let SlotContent::Roll = self.grid[row_idx][col_idx] {
            if self.is_accessible(row_idx, col_idx) {
              self.grid[row_idx][col_idx] = SlotContent::Removed;
              was_any_roll_removed = true;
            }
          }
        }
      }

      if !was_any_roll_removed {
        break; // No more accessible rolls to remove
      }
    }
  }

  /// More efficient variant: use a queue to process accessible rolls and only
  /// re-check neighbors of removed rolls. This avoids repeated full-grid scans.
  pub fn remove_accessible_rolls_queue(&mut self) {
    use std::collections::VecDeque;

    let rows = self.grid.len();
    if rows == 0 {
      return;
    }
    let cols = self.grid[0].len();

    // Track which positions have been enqueued to avoid duplicates.
    let mut enqueued = vec![vec![false; cols]; rows];
    let mut q: VecDeque<(usize, usize)> = VecDeque::new();

    // Seed queue with all currently-accessible rolls.
    for r in 0..rows {
      for c in 0..cols {
        if let SlotContent::Roll = self.grid[r][c] {
          if self.is_accessible(r, c) {
            q.push_back((r, c));
            enqueued[r][c] = true;
          }
        }
      }
    }

    // Process queue: remove accessible rolls and enqueue neighbors that might become accessible.
    while let Some((r, c)) = q.pop_front() {
      // We popped (r,c) from the queue; mark it as not in-queue so it can be
      // re-enqueued later if it is not removed now but becomes accessible after
      // further neighbor removals.
      enqueued[r][c] = false;

      // Only act if it's still a Roll and currently accessible under grid state.
      if let SlotContent::Roll = self.grid[r][c] {
        if self.is_accessible(r, c) {
          // Remove the roll.
          self.grid[r][c] = SlotContent::Removed;

          // Enqueue all 8 neighbors (if they are rolls and not already enqueued).
          for dr in -1isize..=1 {
            for dc in -1isize..=1 {
              if dr == 0 && dc == 0 {
                continue;
              }
              let nr = r as isize + dr;
              let nc = c as isize + dc;
              if nr >= 0 && nr < rows as isize && nc >= 0 && nc < cols as isize {
                let (nr_u, nc_u) = (nr as usize, nc as usize);
                if !enqueued[nr_u][nc_u] {
                  if let SlotContent::Roll = self.grid[nr_u][nc_u] {
                    q.push_back((nr_u, nc_u));
                    enqueued[nr_u][nc_u] = true;
                  }
                }
              }
            }
          }
        }
      }
    }
  }
}
