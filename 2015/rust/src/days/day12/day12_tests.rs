#![allow(unused)]
use crate::{
  common::{AocExample, PuzzleInputProvider, SteppedSolver, YEAR_NUM},
  days::day12::Day12Solver,
};

const DAY_NUM: i32 = 12;

#[test]
fn sample_01_part_one() {
  let raw_input = vec!["[1,2,3]"];
  let expected_output: i64 = 6;

  let solver = Day12Solver {
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
  const EXPECTED_SOLUTION_PART1: i64 = 191164;

  let solver = Day12Solver {
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

  let solver = Day12Solver {
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

  let solver = Day12Solver {
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
      raw_input: vec!["{\"a\":2,\"b\":4}"],
      expected_output: 6,
    },
    AocExample {
      raw_input: vec!["[1,2,3]"],
      expected_output: 6,
    },
    AocExample {
      raw_input: vec!["[[[3]]]"],
      expected_output: 3,
    },
    AocExample {
      raw_input: vec!["{\"a\":{\"b\":4},\"c\":-1}"],
      expected_output: 3,
    },
    AocExample {
      raw_input: vec!["{\"a\":[-1,1]}"],
      expected_output: 0,
    },
    AocExample {
      raw_input: vec!["[-1,{\"a\":1}]"],
      expected_output: 0,
    },
    AocExample {
      raw_input: vec!["[]"],
      expected_output: 0,
    },
    AocExample {
      raw_input: vec!["{}"],
      expected_output: 0,
    },
  ];

  let solver = Day12Solver {
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

  let solver = Day12Solver {
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
