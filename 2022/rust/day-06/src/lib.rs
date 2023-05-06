pub fn process_part1(input: &str) -> u32 {
  0
}

pub fn process_part2(input: &str) -> u32 {
  0
}

#[cfg(test)]
mod tests {
  use super::*;
    const INPUT_1: &str = "mjqjpqmgbljsphdztnvjfqwrcgsmlb";
    const INPUT_2: &str = "bvwbjplbgvbhsrlpgdmjqwftvncz";
    const INPUT_3: &str = "nppdvjthqldpwncqszvftbrmjlhg";
    const INPUT_4: &str = "nznrnfrfntjfmvfwmzdfjlvtqnbhcprsg";
    const INPUT_5: &str = "zcfzfwzzqfrljwzlrfnpqdbhtmscgvjw";

  #[test]
  fn part1_1_works() {
    let result = process_part1(INPUT_1);
    assert_eq!(result, 7);
  }
  #[test]
  fn part1_2_works() {
    let result = process_part1(INPUT_2);
    assert_eq!(result, 5);
  }
  #[test]
  fn part1_3_works() {
    let result = process_part1(INPUT_3);
    assert_eq!(result, 6);
  }
  #[test]
  fn part1_4_works() {
    let result = process_part1(INPUT_4);
    assert_eq!(result, 10);
  }
  #[test]
  fn part1_5_works() {
    let result = process_part1(INPUT_5);
    assert_eq!(result, 11);
  }

  #[test]
  #[ignore]
  fn part2_works() {
    let result = process_part2(INPUT_1);
    assert_eq!(result, 45000);
  }
}