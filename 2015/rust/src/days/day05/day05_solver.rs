#![allow(unused)]
use crate::common::SteppedSolver;

#[derive(Debug)]
pub struct Day05Solver {
  pub day: i32,
  pub year: i32,
}

impl SteppedSolver<Vec<String>, Vec<String>, i64, i64> for Day05Solver {
  fn parse_input_one(&self, input: Vec<&str>) -> Vec<String> {
    input.iter().map(|x| x.to_string()).collect()
  }

  fn parse_input_two(&self, input: Vec<&str>) -> Vec<String> {
    self.parse_input_one(input)
  }

  fn solve_part_one(&self, input: Vec<String>) -> i64 {
    let count_nice = input
      .iter()
      .filter(|string_element| {
        let mut vowels = 0;
        let mut has_duplicate = false;
        let mut last_char = ' ';

        for c_char in string_element.chars() {
          match c_char {
            'a' | 'e' | 'i' | 'o' | 'u' => vowels += 1,
            _ => (),
          }

          if c_char == last_char {
            has_duplicate = true;
          }

          if (last_char == 'a' && c_char == 'b')
            || (last_char == 'c' && c_char == 'd')
            || (last_char == 'p' && c_char == 'q')
            || (last_char == 'x' && c_char == 'y')
          {
            return false;
          }

          last_char = c_char;
        }
        vowels >= 3 && has_duplicate
      })
      .count();
    count_nice as i64
  }

  fn solve_part_two(&self, input: Vec<String>) -> i64 {
    let count_nice = input
      .iter()
      .filter(|string_element| {
        let has_non_overlapping_pairs = has_overlapping_pairs(string_element);
        let has_repeated_letter = has_repeated_letter(string_element);
        has_non_overlapping_pairs && has_repeated_letter
      })
      .count();
    count_nice as i64
  }
}

fn has_overlapping_pairs(string_element: &str) -> bool {
  for (index, target_second) in string_element.chars().enumerate() {
    // Skip the first character
    if index < 1 {
      continue;
    }

    let target_first = string_element.chars().nth(index - 1).unwrap();

    for (index2, match_second) in string_element.chars().enumerate() {
      // Skip up to the target pair
      if index2 <= index + 1 {
        continue;
      }
      let match_first = string_element.chars().nth(index2 - 1).unwrap();
      if target_first == match_first && target_second == match_second {
        return true;
      }
    }
  }
  false
}

fn has_repeated_letter(string_element: &str) -> bool {
  let mut last_char_2behind = ' ';
  let mut last_char = ' ';

  for current_char in string_element.chars() {
    if (current_char == last_char_2behind) {
      return true;
    }

    last_char_2behind = last_char;
    last_char = current_char;
  }
  false
}
