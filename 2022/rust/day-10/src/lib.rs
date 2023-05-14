pub fn process_part1(input: &str) -> u32 {
  0
}

pub fn process_part2(input: &str) -> u32 {
  0
}

#[cfg(test)]
mod tests {
  use super::*;
    const INPUT_1: &str = "noop
addx 3
addx -5";

    const INPUT_2: &str = "addx 15
addx -11
addx 6
addx -3
addx 5
addx -1
addx -8
addx 13
addx 4
noop
addx -1
addx 5
addx -1
addx 5
addx -1
addx 5
addx -1
addx 5
addx -1
addx -35
addx 1
addx 24
addx -19
addx 1
addx 16
addx -11
noop
noop
addx 21
addx -15
noop
noop
addx -3
addx 9
addx 1
addx -3
addx 8
addx 1
addx 5
noop
noop
noop
noop
noop
addx -36
noop
addx 1
addx 7
noop
noop
noop
addx 2
addx 6
noop
noop
noop
noop
noop
addx 1
noop
noop
addx 7
addx 1
noop
addx -13
addx 13
addx 7
noop
addx 1
addx -33
noop
noop
noop
addx 2
noop
noop
noop
addx 8
noop
addx -1
addx 2
addx 1
noop
addx 17
addx -9
addx 1
addx 1
addx -3
addx 11
noop
noop
addx 1
noop
addx 1
noop
noop
addx -13
addx -19
addx 1
addx 3
addx 26
addx -30
addx 12
addx -1
addx 3
addx 1
noop
noop
noop
addx -9
addx 18
addx 1
addx 2
noop
noop
addx 9
noop
noop
noop
addx -1
addx 2
addx -37
addx 1
addx 3
noop
addx 15
addx -21
addx 22
addx -6
addx 1
noop
addx 2
addx 1
noop
addx -10
noop
noop
addx 20
addx 1
addx 2
addx 2
addx -6
addx -11
noop
noop
noop";

  #[test]
  fn part1_1_works() {
    let result = process_part1(INPUT_1);
    assert_eq!(result, 24000);
  }

  #[test]
  fn part1_2_works() {
    let result = process_part1(INPUT_2);
    assert_eq!(result, 13140);
  }

  #[test]
  #[ignore]
  fn part2_1_works() {
    let result = process_part2(INPUT_1);
    assert_eq!(result, 45000);
  }

  #[test]
  #[ignore]
  fn part2_2_works() {
    let result = process_part2(INPUT_2);
    assert_eq!(result, 45000);
  }
}