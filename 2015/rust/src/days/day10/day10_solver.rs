#![allow(unused)]
use crate::common::SteppedSolver;

#[derive(Debug)]
pub struct Day10Solver {
  pub day: i32,
  pub year: i32,
  pub steps: Option<i32>,
}

impl SteppedSolver<String, String, i64, i64> for Day10Solver {
  fn parse_input_one(&self, input: Vec<&str>) -> String {
    input.first().unwrap_or(&"").to_string()
  }

  fn parse_input_two(&self, input: Vec<&str>) -> String {
    self.parse_input_one(input)
  }

  fn solve_part_one(&self, input: String) -> i64 {
    let max_steps = self.steps.unwrap_or(40);

    let mut current_input: String = input;
    for step in 0..max_steps {
      current_input = look_and_say(current_input);
    }

    current_input.len() as i64
  }

  fn solve_part_two(&self, input: String) -> i64 {
    let max_steps = self.steps.unwrap_or(50);

    let mut current_input: String = input;
    for step in 0..max_steps {
      current_input = look_and_say(current_input);
    }

    current_input.len() as i64
  }
}

fn look_and_say(current_input: String) -> String {
  let mut chars = current_input.chars();
  let mut current_char = chars.next().unwrap();
  let mut current_count = 1;
  let mut result = String::new();

  for c in chars {
    if c == current_char {
      current_count += 1;
    } else {
      result.push_str(&current_count.to_string());
      result.push(current_char);
      current_char = c;
      current_count = 1;
    }
  }

  result.push_str(&current_count.to_string());
  result.push(current_char);

  result
}
