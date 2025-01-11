#![allow(unused)]
use crate::{
  common::{AocExample, PuzzleInputProvider, SteppedSolver, YEAR_NUM},
  days::day04::Day04Solver,
};

const DAY_NUM: i32 = 4;

#[test]
fn sample_01_part_one() {
  let raw_input = vec!["abcdef"];
  let expected_output: i64 = 609043;

  let solver = Day04Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
  };
  let input = solver.parse_input_one(raw_input);
  let result = solver.solve_part_one(input);

  assert_eq!(result, expected_output);
}

#[test]
fn examples_part_one() {
  let examples: Vec<AocExample<i64>> = vec![AocExample {
    raw_input: vec!["pqrstuv"],
    expected_output: 1048970,
  }];

  let solver = Day04Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
  };

  for (index, example) in examples.iter().enumerate() {
    let input = solver.parse_input_one(example.raw_input.clone());
    let result = solver.solve_part_one(input);

    assert_eq!(
      result, example.expected_output,
      "Failed for Example: {:?}",
      index
    );
  }
}
