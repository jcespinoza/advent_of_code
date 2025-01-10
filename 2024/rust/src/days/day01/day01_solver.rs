use crate::common::solver::SteppedSolver;

#[derive(Debug)]
pub struct Day01Solver {
  pub day: i32,
  pub year: i32,
}

impl SteppedSolver<Vec<i32>, Vec<i32>, i32, i32> for Day01Solver {
  fn parse_input_one(&self, input: Vec<&str>) -> Vec<i32> {
    input.iter().map(|x| x.parse::<i32>().unwrap()).collect()
  }

  fn parse_input_two(&self, input: Vec<&str>) -> Vec<i32> {
    input.iter().map(|x| x.parse::<i32>().unwrap()).collect()
  }

  fn solve_part_one(&self, input: Vec<i32>) -> i32 {
    let mut result = 0;
    for i in 0..input.len() {
      for j in i + 1..input.len() {
        if input[i] + input[j] == 2020 {
          result = input[i] * input[j];
          break;
        }
      }
    }
    result
  }

  fn solve_part_two(&self, input: Vec<i32>) -> i32 {
    let mut result = 0;
    for i in 0..input.len() {
      for j in i + 1..input.len() {
        for k in j + 1..input.len() {
          if input[i] + input[j] + input[k] == 2020 {
            result = input[i] * input[j] * input[k];
            break;
          }
        }
      }
    }
    result
  }
}
