pub fn process_part1(input: &str) -> u32 {
  0
}

pub fn process_part2(input: &str) -> u32 {
  0
}

#[cfg(test)]
mod tests {
  use super::*;
    const INPUT: &str = "Sabqponm
abcryxxl
accszExk
acctuvwj
abdefghi";

  #[test]
  fn part1_works() {
    let result = process_part1(INPUT);
    assert_eq!(result, 31);
  }

  #[test]
  #[ignore]
  fn part2_works() {
    let result = process_part2(INPUT);
    assert_eq!(result, 45000);
  }
}