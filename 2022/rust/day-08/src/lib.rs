pub fn process_part1(input: &str) -> u32 {
  let grid_of_trees:Vec<Vec<u32>> = input.lines().map(|line| {
    line.chars().map(|c| c.to_digit(10).unwrap()).collect()
  }).collect();

  dbg!(&grid_of_trees);

  0
}

pub fn process_part2(input: &str) -> u32 {
  0
}

#[cfg(test)]
mod tests {
  use super::*;
    const INPUT: &str = "30373
25512
65332
33549
35390";

  #[test]
  fn part1_works() {
    let result = process_part1(INPUT);
    assert_eq!(result, 21);
  }

  #[test]
  #[ignore]
  fn part2_works() {
    let result = process_part2(INPUT);
    assert_eq!(result, 45000);
  }
}