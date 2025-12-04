pub struct Range {
  pub start: i64,
  pub end: i64,
}

/// Create a Range from a string in the format "start-end"
impl From<&str> for Range {
  fn from(s: &str) -> Self {
    let parts: Vec<&str> = s.split('-').collect();
    let start = parts[0].parse::<i64>().unwrap();
    let end = parts[1].parse::<i64>().unwrap();
    Range { start, end }
  }
}

pub fn identify_invalid_ids(range: &Range) -> Vec<i64> {
  let mut invalid_ids = Vec::new();
  for id in range.start..=range.end {
    if !is_id_valid(id) {
      invalid_ids.push(id);
    }
  }
  invalid_ids
}

// An id is valid if when their digits are split into two halves, the resulting numbers are different
fn is_id_valid(id: i64) -> bool {
  let id_str = id.to_string();
  let len = id_str.len();

  if !len.is_multiple_of(2) {
    return true; // Odd length ids are considered valid
  }

  let mid = len / 2;
  let first_half = &id_str[..mid];
  let second_half = &id_str[mid..];
  first_half != second_half
}
