use std::{collections::VecDeque, fmt::Display};

pub struct TachyonManifold {
  pub cells: Vec<Vec<CellContent>>,
}

impl Display for TachyonManifold {
  fn fmt(&self, f: &mut std::fmt::Formatter<'_>) -> std::fmt::Result {
    for row in &self.cells {
      for cell in row {
        let ch = match cell {
          CellContent::Empty => ".",
          CellContent::Splitter => "^",
          CellContent::Start => "S",
          CellContent::Beam => "|",
        };
        write!(f, "{}", ch)?;
      }
      writeln!(f)?;
    }
    Ok(())
  }
}

impl TachyonManifold {
  pub(crate) fn extend_tachyon_beams(&mut self) -> () {
    // First find the start position. It's the cell with CellContent::Start
    let mut start_pos = None;
    for (row_idx, row) in self.cells.iter().enumerate() {
      for (col_idx, cell) in row.iter().enumerate() {
        if *cell == CellContent::Start {
          start_pos = Some((row_idx as i64, col_idx as i64));
          break;
        }
      }
    }
    let (start_row, start_col) = start_pos.expect("Start position not found");
    // Create a queue to hold the beams that need to be extended, starting with the locatino of the start position
    let mut beam_queue = VecDeque::new();
    beam_queue.push_back((start_row, start_col)); // (row, col, direction_index)

    // Next, for every cell in the queue until the queue is empty, extend the beam downwards until it either hits a splitter or goes out of bounds. If the cell is empty mark it as Beam.
    while let Some((row, col)) = beam_queue.pop_front() {
      let mut current_row = row + 1; // Start extending downwards
      while current_row < self.cells.len() as i64 {
        match self.cells[current_row as usize][col as usize] {
          CellContent::Empty => {
            self.cells[current_row as usize][col as usize] = CellContent::Beam;
            current_row += 1; // Continue downwards
          }
          CellContent::Splitter => {
            // Mark the cells to the left and right as new beams to extend. Need to add them to the queue and also change the content of both to Beam
            // Add left and right positions to the queue
            if col > 0 {
              self.cells[current_row as usize][(col - 1) as usize] = CellContent::Beam;
              beam_queue.push_back((current_row, col - 1));
            }
            if col < (self.cells[0].len() as i64 - 1) {
              self.cells[current_row as usize][(col + 1) as usize] = CellContent::Beam;
              beam_queue.push_back((current_row, col + 1));
            }
            break;
          }
          _ => {
            break;
          }
        }
      }
    }
  }

  pub(crate) fn count_hit_splitters(&self) -> i64 {
    // Need to find all cells that have a CellContent::Beam to the left and right of them
    // Also they must have a CellContent::Beam in the cell above them to count
    let mut hit_splitter_count = 0;
    for (row_idx, row) in self.cells.iter().enumerate() {
      for (col_idx, cell) in row.iter().enumerate() {
        if *cell == CellContent::Splitter {
          // Check left and right
          let left_hit = if col_idx > 0 {
            self.cells[row_idx][col_idx - 1] == CellContent::Beam
          } else {
            false
          };
          let right_hit = if col_idx < row.len() - 1 {
            self.cells[row_idx][col_idx + 1] == CellContent::Beam
          } else {
            false
          };
          let above_hit = if row_idx > 0 {
            self.cells[row_idx - 1][col_idx] == CellContent::Beam
          } else {
            false
          };
          if left_hit && right_hit && above_hit {
            hit_splitter_count += 1;
          }
        }
      }
    }
    hit_splitter_count
  }
}

#[derive(Debug, Clone, PartialEq)]
pub enum CellContent {
  Empty,
  Splitter,
  Start,
  Beam,
}

impl From<&str> for TachyonManifold {
  fn from(input: &str) -> Self {
    let mut cells = Vec::new();

    for line in input.lines() {
      let mut row = Vec::new();
      for ch in line.chars() {
        let cell = CellContent::from(&ch.to_string()[..]);
        row.push(cell);
      }
      cells.push(row);
    }

    TachyonManifold { cells }
  }
}

impl From<&str> for CellContent {
  fn from(ch: &str) -> Self {
    match ch {
      "." => CellContent::Empty,
      "^" => CellContent::Splitter,
      "S" => CellContent::Start,
      "|" => CellContent::Beam,
      _ => panic!("Invalid cell character: {}", ch),
    }
  }
}
