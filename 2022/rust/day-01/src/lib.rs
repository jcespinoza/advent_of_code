pub fn process_part1(input: &str) -> u32 {
  let groups_of_lines = input
  .split("\n\n");

  let sums_of_calories = groups_of_lines
  .map(|elf_load|{
    elf_load
      .lines()
      .map(|item| item.trim().parse::<u32>().unwrap() )
        .sum::<u32>()
  });

  let result =  sums_of_calories
  .max()
  .unwrap();
  
  result
}

pub fn process_part2(input: &str) -> u32 {
  let groups_of_lines = input
  .split("\n\n");

  let sums_of_calories = groups_of_lines
  .map(|elf_load|{
    elf_load
      .lines()
      .map(|item| item.trim().parse::<u32>().unwrap() )
        .sum::<u32>()
  });

  let mut result: Vec<u32> =  sums_of_calories
  .collect();

  result.sort_by(|a,b| b.cmp(a));

  result.iter().take(3).sum()
  
}

#[cfg(test)]
mod tests {
  use super::*;
    const INPUT: &str = "1000
    2000
    3000

    4000

    5000
    6000

    7000
    8000
    9000

    10000";

  #[test]
  fn part1_works() {
    let result = process_part1(INPUT);
    assert_eq!(result, 24000);
  }

  #[test]
  fn part2_works() {
    let result = process_part2(INPUT);
    assert_eq!(result, 45000);
  }
}