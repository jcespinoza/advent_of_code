#![allow(unused)]
use crate::{
  common::{AocExample, PuzzleInputProvider, SteppedSolver, YEAR_NUM},
  days::day06::Day06Solver,
};

const DAY_NUM: i32 = 6;

#[test]
fn sample_01_part_one() {
  let raw_input = vec!["turn on 0,0 through 999,999"];
  let expected_output: i64 = 1_000_000;

  let solver = Day06Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
  };
  let input = solver.parse_input_one(raw_input);
  let result = solver.solve_part_one(input);

  assert_eq!(result, expected_output);
}

#[test]
fn solution_part_one() {
  let remote_input = PuzzleInputProvider::new_remote(YEAR_NUM, DAY_NUM)
    .read_input()
    .unwrap();
  let raw_input = remote_input.iter().map(|x| x.as_str()).collect();
  const EXPECTED_SOLUTION_PART1: i64 = 569999;

  let solver = Day06Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
  };
  let input = solver.parse_input_one(raw_input);
  let result = solver.solve_part_one(input);

  assert_eq!(result, EXPECTED_SOLUTION_PART1);
}

#[test]
fn sample_01_part_two() {
  let raw_input = vec!["toggle 0,0 through 999,999"];
  let expected_output: i64 = 2_000_000;

  let solver = Day06Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
  };
  let input = solver.parse_input_two(raw_input);
  let result = solver.solve_part_two(input);

  assert_eq!(result, expected_output);
}

#[test]
fn solution_part_two() {
  let remote_input = PuzzleInputProvider::new_remote(YEAR_NUM, DAY_NUM)
    .read_input()
    .unwrap();
  let raw_input = remote_input.iter().map(|x| x.as_str()).collect();
  const EXPECTED_SOLUTION_PART2: i64 = 17836115;

  let solver = Day06Solver {
    day: DAY_NUM,
    year: YEAR_NUM,
  };
  let input = solver.parse_input_two(raw_input);
  let result = solver.solve_part_two(input);

  assert_eq!(result, EXPECTED_SOLUTION_PART2);
}

#[test]
fn examples_part_one() {
  let examples: Vec<AocExample<i64>> = vec![
    AocExample {
      raw_input: vec!["toggle 0,0 through 999,0"],
      expected_output: 1000,
    },
    AocExample {
      raw_input: vec![
        "turn on 0,0 through 999,999",
        "turn off 499,499 through 500,500",
      ],
      expected_output: 1_000_000 - 4,
    },
  ];

  let solver = Day06Solver {
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
fn examples_part_two() {
  let examples: Vec<AocExample<i64>> = vec![AocExample {
    raw_input: vec!["turn on 0,0 through 0,0"],
    expected_output: 1,
  }];

  let solver = Day06Solver {
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
