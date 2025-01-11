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
    let count_nice = input.iter().filter(|x| is_nice(x)).count();
    count_nice as i64
  }

  fn solve_part_two(&self, input: Vec<String>) -> i64 {
    unimplemented!()
  }
}

fn is_nice(x: &str) -> bool {
  let mut vowels = 0;
  let mut has_duplicate = false;
  let mut last_char = ' ';

  for c_char in x.chars() {
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
}
