use nom::{multi::separated_list1, character::complete::{newline, self}, sequence::separated_pair, bytes::complete::tag};

pub fn process_part1(input: &str) -> u32 {
  let motions = parse_motions(input);


  println!("{:?}", motions);

  0
}

fn parse_motions(input: &str) -> Vec<Motion> {
    let (_, motions) = separated_list1(newline, motion_parser)(input).unwrap();
    motions
}

pub fn process_part2(input: &str) -> u32 {
  0
}

fn motion_parser(input: &str) -> nom::IResult<&str, Motion> {
  let (input, (direction_char, qty)) = separated_pair(complete::one_of("RULD"), tag(" "), complete::u32)(input)?;
  let direction = match direction_char {
    'R' => Direction::Right,
    'L' => Direction::Left,
    'U' => Direction::Up,
    'D' => Direction::Down,
    _ => panic!("Invalid direction")
    };
  Ok((input, Motion { direction, qty }))
}

#[derive(Debug, PartialEq)]
struct Motion {
  direction: Direction,
  qty: u32
}

#[derive(Debug, PartialEq)]
enum Direction {
  Up,
  Down,
  Left,
  Right
}

#[derive(Debug, PartialEq)]
struct Position {
  x: i32,
  y: i32
}

#[cfg(test)]
mod tests {
  use super::*;
    const INPUT: &str = "R 4
U 4
L 3
D 1
R 4
D 1
L 5
R 2";

  #[test]
  fn part1_works() {
    let result = process_part1(INPUT);
    assert_eq!(result, 24000);
  }

  #[test]
  #[ignore]
  fn part2_works() {
    let result = process_part2(INPUT);
    assert_eq!(result, 45000);
  }
}