use crate::days::day03::Point;

#[derive(Debug, PartialEq)]
pub enum Instruction {
  TurnOn(Point, Point),
  TurnOff(Point, Point),
  Toggle(Point, Point),
}

impl Instruction {
  pub fn parse(input: &str) -> Instruction {
    parse_instruction(input)
  }
}

pub fn parse_instruction(input: &str) -> Instruction {
  let mut parts = input.split(" through ");
  let first_part = parts.next().unwrap();
  let end = parse_point(parts.last().unwrap());

  if first_part.starts_with("turn on") {
    let start = parse_point(first_part.trim_start_matches("turn on "));
    Instruction::TurnOn(start, end)
  } else if first_part.starts_with("turn off") {
    let start = parse_point(first_part.trim_start_matches("turn off "));
    Instruction::TurnOff(start, end)
  } else {
    let start = parse_point(first_part.trim_start_matches("toggle "));
    Instruction::Toggle(start, end)
  }
}

fn parse_point(start: &str) -> Point {
  let mut parts = start.split(',');
  let col = parts.next().unwrap().parse().unwrap();
  let row = parts.last().unwrap().parse().unwrap();

  Point::new(row, col)
}
