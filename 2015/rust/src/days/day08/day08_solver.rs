#![allow(unused)]
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
    let pairs = input.iter().map(|line| {
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

      (code_sum, memory_sum)
    });

    let mut total_code_sum = 0;
    let mut total_memory_sum = 0;
    for (code_sum, memory_sum) in pairs {
      total_code_sum += code_sum;
      total_memory_sum += memory_sum;
    }

    total_code_sum - total_memory_sum
  }

  fn solve_part_two(&self, input: Vec<String>) -> i64 {
    unimplemented!()
  }
}
