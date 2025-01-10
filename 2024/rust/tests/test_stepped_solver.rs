use advent_of_code::common::solver::SteppedSolver;

struct Day00Sample {}

impl SteppedSolver<Vec<i32>, Vec<i32>, i64, i64> for Day00Sample {
  fn parse_input_one(&self, input: Vec<&str>) -> Vec<i32> {
    input.iter().map(|x| x.parse::<i32>().unwrap()).collect()
  }

  fn parse_input_two(&self, input: Vec<&str>) -> Vec<i32> {
    self.parse_input_one(input)
  }

  fn solve_part_one(&self, input: Vec<i32>) -> i64 {
    input.iter().map(|x| *x as i64).sum()
  }

  fn solve_part_two(&self, input: Vec<i32>) -> i64 {
    input.iter().map(|x| *x as i64).sum()
  }
}

impl Day00Sample {
  pub fn new() -> Day00Sample {
    Day00Sample {}
  }
}

#[test]
fn test_stepped_solver() {
  let solver = Day00Sample::new();

  let raw_input = vec!["1", "2", "3", "4", "5", "6", "7", "8", "9", "10"];
  let expected_sum = 55;

  let input = solver.parse_input_one(raw_input);

  let result = solver.solve_part_one(input);

  assert_eq!(result, expected_sum);
}
