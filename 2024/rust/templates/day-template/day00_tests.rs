#![allow(unused)]
use crate::{
  common::{AocExample, PuzzleInputProvider, SteppedSolver, YEAR_NUM},
  days::day00::Day00Solver,
};

const DAY_NUM: i32 = 0;

#[test]
fn sample_01_part_one() {
  let raw_input = vec![];
  let expected_output: i64 = 0;

  let solver = Day00Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
  };
  let input = solver.parse_input_one(raw_input);
  let result = solver.solve_part_one(input);

  assert_eq!(result, expected_output);
}

#[test]
#[ignore]
fn solution_part_one() {
  #[allow(dead_code)]
  let remote_input = PuzzleInputProvider::new_remote(YEAR_NUM, DAY_NUM)
    .read_input()
    .unwrap();
  let raw_input = remote_input.iter().map(|x| x.as_str()).collect();
  const EXPECTED_SOLUTION_PART1: i64 = 0;

  let solver = Day00Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
  };
  let input = solver.parse_input_one(raw_input);
  let result = solver.solve_part_one(input);

  assert_eq!(result, EXPECTED_SOLUTION_PART1);
}

#[test]
#[ignore]
fn sample_01_part_two() {
  let raw_input = vec![];
  let expected_output: i64 = 0;

  let solver = Day00Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
  };
  let input = solver.parse_input_two(raw_input);
  let result = solver.solve_part_two(input);

  assert_eq!(result, expected_output);
}

#[test]
#[ignore]
fn solution_part_two() {
  let remote_input = PuzzleInputProvider::new_remote(YEAR_NUM, DAY_NUM)
    .read_input()
    .unwrap();
  let raw_input = remote_input.iter().map(|x| x.as_str()).collect();
  const EXPECTED_SOLUTION_PART2: i64 = 0;

  let solver = Day00Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
  };
  let input = solver.parse_input_two(raw_input);
  let result = solver.solve_part_two(input);

  assert_eq!(result, EXPECTED_SOLUTION_PART2);
}

#[test]
#[ignore]
fn examples_part_one() {
  let examples: Vec<AocExample<i64>> = vec![AocExample {
    raw_input: vec![],
    expected_output: 0,
  }];

  let solver = Day00Solver {
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

#[test]
#[ignore]
fn examples_part_two() {
  let examples: Vec<AocExample<i64>> = vec![AocExample {
    raw_input: vec![],
    expected_output: 0,
  }];

  let solver = Day00Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
  };

  for (index, example) in examples.iter().enumerate() {
    let input = solver.parse_input_two(example.raw_input.clone());
    let result = solver.solve_part_two(input);

    assert_eq!(
      result, example.expected_output,
      "Failed for Example: {:?}",
      index
    );
  }
}
