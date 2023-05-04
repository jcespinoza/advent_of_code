use nom::{sequence::separated_pair, character::complete::newline};

pub fn process_part1(input: &str) -> String {
  let two_parts = input.split
  "0".to_string()
}

pub fn process_part2(input: &str) -> String {
  "0".to_string()
}

#[derive(Debug)]
struct CrateStack {
  name: String,
  crates: Vec<char>,
}

struct MoveProcedure {
  qty: u32,
  from: String,
  to: String,
}

#[cfg(test)]
mod tests {
  use super::*;
    const INPUT: &str = "    [D]    
    [N] [C]    
    [Z] [M] [P]
     1   2   3 
    
    move 1 from 2 to 1
    move 3 from 1 to 3
    move 2 from 2 to 1
    move 1 from 1 to 2";

  #[test]
  fn part1_works() {
    let result = process_part1(INPUT);
    assert_eq!(result, "CMZ");
  }

  #[test]
  #[ignore]
  fn part2_works() {
    let result = process_part2(INPUT);
    assert_eq!(result, "45000");
  }
}