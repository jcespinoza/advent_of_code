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
    let pairs = get_usage_details(input_refs);

    let mut total_code_sum = 0;
    let mut total_memory_sum = 0;
    for (_, (code_sum, memory_sum, count)) in pairs {
      total_code_sum += code_sum * count;
      total_memory_sum += memory_sum * count;
    }

    (total_code_sum - total_memory_sum) as i64
  }

  fn solve_part_two(&self, input: Vec<String>) -> i64 {
    unimplemented!()
  }
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
      Some((_, _, count)) => {
        *count += 1;
      }
      None => {
        usage_details.insert(line, (code_sum, memory_sum, 1));
      }
    }
  }
  usage_details
}
