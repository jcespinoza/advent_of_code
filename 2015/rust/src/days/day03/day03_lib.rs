use num_traits::Num;

#[derive(Eq, Hash, PartialEq, Copy, Clone)]
pub struct Offset {
  pub row: i32,
  pub col: i32,
}

#[derive(Debug, Eq, Hash, PartialEq, Copy, Clone)]
pub struct Point {
  pub row: i32,
  pub col: i32,
}

impl Point {
  pub fn new(row: i32, col: i32) -> Self {
    Self { row, col }
  }

  pub fn from((row, col): (i32, i32)) -> Self {
    Self { row, col }
  }

  pub fn move_by(&self, offset: Offset) -> Self {
    Self::from((self.row + offset.row, self.col + offset.col))
  }
}

impl Offset {
  pub fn new(row: i32, col: i32) -> Self {
    Self { row, col }
  }

  pub fn from(c: char) -> Self {
    match c {
      '^' => Self::new(-1, 0),
      'v' => Self::new(1, 0),
      '>' => Self::new(0, 1),
      '<' => Self::new(0, -1),
      _ => Self::new(0, 0),
    }
  }
}
