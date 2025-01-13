#[derive(Debug, Hash, PartialEq, Clone)]
pub struct Segment {
  pub from: String,
  pub to: String,
  pub distance: i32,
}

pub enum Extreme {
  Shortest,
  Longest,
}
