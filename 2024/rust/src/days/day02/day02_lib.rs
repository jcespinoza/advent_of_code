pub struct Report {
  pub levels: Vec<i32>,
}

impl Report {
  pub fn new(levels: Vec<i32>) -> Self {
    Self { levels }
  }

  pub fn is_safe(&self) -> bool {
    if self.levels.len() <= 2 {
      return false;
    }
    let mut max_difference = 0;
    let mut direction_found = false;
    let mut direction = 0;

    for index in 0..self.levels.len() {
      if index == self.levels.len() - 1 {
        break;
      }

      let difference = self.levels.get(index).unwrap() - self.levels.get(index + 1).unwrap();

      let abs_diff = difference.abs();

      if abs_diff > 3 || abs_diff == 0 {
        return false;
      }

      if abs_diff > max_difference {
        max_difference = abs_diff;
      }

      if !direction_found {
        direction = difference.signum();
        direction_found = true;
      }

      if direction_found && direction != difference.signum() {
        return false;
      }
    }

    max_difference <= 3
  }
}

#[cfg(test)]
mod tests {
  use crate::days::day02::Report;

  #[test]
  fn is_safe_is_true_when_there_are_two_levels_or_less() {
    let report = Report::new(vec![1]);
    assert!(!report.is_safe());
  }

  #[test]
  fn is_safe_should_return_true_when_all_levels_are_increasing() {
    let report = Report::new(vec![1, 2, 3, 4, 5]);
    assert!(report.is_safe());
  }

  #[test]
  fn is_safe_should_return_true_when_all_levels_are_decreasing() {
    let report = Report::new(vec![5, 4, 3, 2, 1]);
    assert!(report.is_safe());
  }

  #[test]
  fn is_safe_should_return_true_when_maximum_difference_is_lower_than_three() {
    let report = Report::new(vec![1, 4, 7, 8, 9]);
    assert!(report.is_safe());
  }

  #[test]
  fn is_safe_should_return_false_when_maximum_difference_is_higher_than_three() {
    let report = Report::new(vec![1, 2, 7, 8, 9]);
    assert!(!report.is_safe());
  }

  #[test]
  fn is_safe_should_return_false_when_there_is_an_increase_and_decrease() {
    let report = Report::new(vec![1, 3, 2, 4, 5]);
    assert!(!report.is_safe());
  }

  #[test]
  fn is_safe_should_return_false_when_there_is_an_adjacent_pair_with_no_difference() {
    let report = Report::new(vec![1, 1, 2, 4, 5]);
    assert!(!report.is_safe());
  }
}
