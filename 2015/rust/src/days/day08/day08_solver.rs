#![allow(unused)]
use std::collections::HashMap;

use crate::common::SteppedSolver;

#[derive(Debug)]
pub struct Day08Solver {
  pub day: i32,
  pub year: i32,
}

impl SteppedSolver<Vec<String>, Vec<String>, i64, i64> for Day08Solver {
  fn parse_input_one(&self, input: Vec<&str>) -> Vec<String> {
    input.iter().map(|x| x.to_string()).collect()
  }

  fn parse_input_two(&self, input: Vec<&str>) -> Vec<String> {
    self.parse_input_one(input)
  }

  fn solve_part_one(&self, input: Vec<String>) -> i64 {
    let input_refs: Vec<&str> = input.iter().map(|s| s.as_str()).collect();
    let usage_map = get_usage_details(input_refs);

    let mut total_code_sum = 0;
    let mut total_memory_sum = 0;
    for (_, (code_sum, memory_sum, frequency)) in usage_map {
      total_code_sum += code_sum * frequency;
      total_memory_sum += memory_sum * frequency;
    }

    (total_code_sum - total_memory_sum) as i64
  }

  fn solve_part_two(&self, input: Vec<String>) -> i64 {
    let input_refs: Vec<&str> = input.iter().map(|s| s.as_str()).collect();
    let usage_map = get_usage_details(input_refs);

    let mut total_encoded_sum = 0;
    let mut total_original_sum = 0;
    for (line, (code_sum, _, frequency)) in usage_map {
      let encoded_length = find_encoded_length(line);
      total_encoded_sum += encoded_length * frequency;
      total_original_sum += code_sum * frequency;
    }

    (total_encoded_sum - total_original_sum) as i64
  }
}

fn find_encoded_length(line: &str) -> i32 {
  let mut encoded_length = 2; // The opening and closing quotes
  for c in line.chars() {
    if c == '"' || c == '\\' {
      // " becomes \", \ becomes \\, therefore 2 characters
      encoded_length += 2;
    } else {
      // All other characters remain the same
      encoded_length += 1;
    }
  }
  encoded_length
}

fn get_usage_details(input: Vec<&str>) -> HashMap<&str, (i32, i32, i32)> {
  let mut usage_details: HashMap<&str, (i32, i32, i32)> = HashMap::new();

  for &line in input.iter() {
    let mut code_sum = 0;
    let mut memory_sum = 0;

    let mut chars = line.chars();
    while let Some(c) = chars.next() {
      code_sum += 1;
      if c == '"' {
        continue;
      }

      if c == '\\' {
        let next = chars.next().unwrap();
        code_sum += 1;
        if next == 'x' {
          chars.next();
          chars.next();
          code_sum += 2;
        }
      }

      memory_sum += 1;
    }

    match usage_details.get_mut(line) {
      Some((_, _, frequency)) => {
        *frequency += 1;
      }
      None => {
        usage_details.insert(line, (code_sum, memory_sum, 1));
      }
    }
  }
  usage_details
}
