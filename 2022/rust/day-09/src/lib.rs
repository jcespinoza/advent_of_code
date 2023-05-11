use std::collections::HashSet;

use nom::{multi::separated_list1, character::complete::{newline, self}, sequence::separated_pair, bytes::complete::tag};

pub fn process_part1(input: &str) -> u32 {
  let motions = parse_motions(input);

  let mut visited_positions: HashSet<Position> = HashSet::new();

  let mut head_position = Position { x: 0, y: 0 };
  let mut tail_position = head_position.clone();

  for motion in motions {
    for _  in 0..motion.qty {
      let target_position = head_position.move_in_direction_by_copy(&motion.direction);
      if !positions_are_adjacent(&target_position, &tail_position) {
        tail_position = head_position.clone();
      }
      head_position.move_in_direction(&motion.direction);
      
      visited_positions.insert(tail_position.clone());
    }
  }

  visited_positions.len() as u32
}

pub fn process_part2(input: &str) -> u32 {
  0
}

fn parse_motions(input: &str) -> Vec<Motion> {
    let (_, motions) = separated_list1(newline, motion_parser)(input).unwrap();
    motions
}

fn positions_are_adjacent(pos_1: &Position, pos_2: &Position) -> bool {
    pos_1.x == pos_2.x && (pos_1.y - pos_2.y).abs() == 1
    || pos_1.y == pos_2.y && (pos_1.x - pos_2.x).abs() == 1
    || (pos_1.x - pos_2.x).abs() == 1 && (pos_1.y - pos_2.y).abs() == 1
    || pos_1.x == pos_2.x && pos_1.y == pos_2.y
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

#[derive(Debug, PartialEq, Clone, Eq, Hash)]
struct Position {
  x: i32,
  y: i32
}

impl Position {
  fn move_in_direction(&mut self, direction: &Direction) {
    match direction {
      Direction::Up => self.y += 1,
      Direction::Down => self.y -= 1,
      Direction::Left => self.x -= 1,
      Direction::Right => self.x += 1
    }
  }
  fn move_in_direction_by_copy(&self, direction: &Direction) -> Position {
    let mut position = self.clone();
    position.move_in_direction(direction);
    position
  }
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
    assert_eq!(result, 13);
  }

  #[test]
  #[ignore]
  fn part2_works() {
    let result = process_part2(INPUT);
    assert_eq!(result, 45000);
  }
}