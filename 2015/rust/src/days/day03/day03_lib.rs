use num_traits::Num;

#[derive(Eq, Hash, PartialEq)]
pub struct Offset {
  pub row: i32,
  pub col: i32,
}

#[derive(Eq, Hash, PartialEq, Copy, Clone)]
pub struct Point<T> {
  pub row: T,
  pub col: T,
}

impl<T> Point<T>
where
  T: Num,
{
  pub fn new(row: T, col: T) -> Self {
    Self { row, col }
  }

  pub fn from((row, col): (T, T)) -> Self {
    Self { row, col }
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
