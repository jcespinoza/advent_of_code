#![allow(unused)]
use crate::common::SteppedSolver;

#[derive(Debug)]
pub struct Day04Solver {
  pub day: i32,
  pub year: i32,
}

impl SteppedSolver<String, String, i64, i64> for Day04Solver {
  fn parse_input_one(&self, input: Vec<&str>) -> String {
    input.first().unwrap().to_string()
  }

  fn parse_input_two(&self, input: Vec<&str>) -> String {
    self.parse_input_one(input)
  }

  fn solve_part_one(&self, secret_key: String) -> i64 {
    let mut integer = 0;
    loop {
      let new_string = format!("{}{}", secret_key, integer);
      let hash = md5::compute(new_string);
      if first_five_are_zeroes(hash) {
        return integer;
      }
      integer += 1;
    }
    integer
  }

  fn solve_part_two(&self, secret_key: String) -> i64 {
    unimplemented!()
  }
}

fn first_five_are_zeroes(hash: md5::Digest) -> bool {
  let first_five = &format!("{:x}", hash)[0..5];
  first_five == "00000"
}
