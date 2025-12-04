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

/// Validation strategy enum to determine which validation rule to apply
pub enum ValidationStrategy {
  HalvesMatch,      // Part one: check if two halves match
  RepeatingPattern, // Part two: check for repeating patterns
}

/// Generic function to identify invalid IDs based on a validation strategy
pub fn identify_invalid_ids(range: &Range, strategy: ValidationStrategy) -> Vec<i64> {
  let mut invalid_ids = Vec::new();
  for id in range.start..=range.end {
    if !is_id_valid(id, &strategy) {
      invalid_ids.push(id);
    }
  }
  invalid_ids
}

/// Check if an ID is valid based on the specified validation strategy
fn is_id_valid(id: i64, strategy: &ValidationStrategy) -> bool {
  match strategy {
    ValidationStrategy::HalvesMatch => is_valid_halves_match(id),
    ValidationStrategy::RepeatingPattern => is_valid_no_repeating_pattern(id),
  }
}

/// Part one validation: An id is valid if when their digits are split into two halves,
/// the resulting numbers are different
fn is_valid_halves_match(id: i64) -> bool {
  let id_str = id.to_string();
  let len = id_str.len();

  if !len.is_multiple_of(2) {
    return true; // Odd length ids are considered valid because their halves could never match
  }

  let mid = len / 2;
  let first_half = &id_str[..mid];
  let second_half = &id_str[mid..];
  first_half != second_half
}

/// Part two validation: An ID is invalid if it is made only of some sequence of digits
/// repeated at least twice
fn is_valid_no_repeating_pattern(id: i64) -> bool {
  // We need to look for patterns of length 1 to half the length of the id
  let id_str = id.to_string();
  let len = id_str.len();

  for pattern_len in 1..=(len / 2) {
    if !len.is_multiple_of(pattern_len) {
      continue; // The pattern length must divide the total length; for example, an id of length 7 cannot be made of repeating patterns of length 3
    }
    let pattern = &id_str[..pattern_len];
    let mut repeated = String::new();
    let repeat_count = len / pattern_len;
    for _ in 0..repeat_count {
      repeated.push_str(pattern);
    }
    if repeated == id_str {
      return false; // Invalid ID found
    }
  }

  true // No invalid patterns found
}
