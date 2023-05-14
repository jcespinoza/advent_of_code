use std::{collections::HashSet, fmt::Display};

use nom::{
    bytes::complete::tag,
    character::complete::{self, newline},
    multi::separated_list1,
    sequence::separated_pair,
};

pub fn process_part1(input: &str) -> u32 {
    let motions = parse_motions(input);

    let mut rope = Rope::new(2);

    let visited_positions = rope.execute_motions(&motions);

    visited_positions
}

pub fn process_part2(input: &str) -> u32 {
    let motions = parse_motions(input);

    let mut rope = Rope::new(10);

    let visited_positions = rope.execute_motions(&motions);

    visited_positions
}

fn parse_motions(input: &str) -> Vec<Motion> {
    let (_, motions) = separated_list1(newline, motion_parser)(input).unwrap();
    motions
}

fn motion_parser(input: &str) -> nom::IResult<&str, Motion> {
    let (input, (direction_char, qty)) =
        separated_pair(complete::one_of("RULD"), tag(" "), complete::u32)(input)?;
    let direction = match direction_char {
        'R' => Direction::Right,
        'L' => Direction::Left,
        'U' => Direction::Up,
        'D' => Direction::Down,
        _ => panic!("Invalid direction"),
    };
    Ok((input, Motion { direction, qty }))
}

#[derive(Debug, PartialEq)]
struct Motion {
    direction: Direction,
    qty: u32,
}

#[derive(Debug, PartialEq)]
enum Direction {
    Up,
    Down,
    Left,
    Right,
}

#[derive(Debug, PartialEq, Clone, Eq, Hash)]
struct Position {
    x: i32,
    y: i32,
}

impl Display for Position {
    fn fmt(&self, f: &mut std::fmt::Formatter<'_>) -> std::fmt::Result {
        write!(f, "({}, {})", self.x, self.y)
    }
}

impl Position {
    fn move_in_direction(&mut self, direction: &Direction) {
        match direction {
            Direction::Up => self.y += 1,
            Direction::Down => self.y -= 1,
            Direction::Left => self.x -= 1,
            Direction::Right => self.x += 1,
        }
    }
    
    fn follow(&mut self, x: i32, y: i32) {
        let dx = x - self.x;
        let dy = y - self.y;

        if dx.abs() > 1 || dy.abs() > 1 {
            self.x = self.x + dx.signum();
            self.y = self.y + dy.signum();
        }
    }
}

#[derive(Debug)]
struct Rope {
    knot_count: u32,
    knots: Vec<Position>,
}

impl Display for Rope {
    fn fmt(&self, f: &mut std::fmt::Formatter<'_>) -> std::fmt::Result {
        for (index, knot) in self.knots.iter().enumerate() {
            _ = write!(f, "{} ({},{}) ", index, knot.x, knot.y);
        }
        write!(f, "")
    }
}

impl Rope {
    fn new(knot_count: u32) -> Self {
        Self {
            knot_count,
            knots: vec![Position { x: 0, y: 0 }; knot_count as usize],
        }
    }

    fn execute_motions(&mut self, motions: &Vec<Motion>) -> u32 {
        let mut visited_positions: HashSet<Position> = HashSet::new();

        for motion in motions {
            // println!(">>>>>>>>>>>>>>>>> Motion: {:?}", motion);
            for _ in 0..motion.qty {
                self.knots[0].move_in_direction(&motion.direction);
                let mut front_knot = self.knots[0].clone();

                for knot_index in 1..self.knot_count {
                    (self.knots[knot_index as usize]).follow(front_knot.x, front_knot.y);

                    let curren_knot = self.knots[knot_index as usize].clone();

                    if knot_index == self.knot_count - 1 {
                        visited_positions.insert(curren_knot.clone());
                    }
                    front_knot = curren_knot;
                }
                // println!("{}", self);
            }
        }

        visited_positions.len() as u32
    }
}

#[cfg(test)]
mod tests {
    use super::*;
    const INPUT_1: &str = "R 4
U 4
L 3
D 1
R 4
D 1
L 5
R 2";
    const INPUT_2: &str = "R 5
U 8
L 8
D 3
R 17
D 10
L 25
U 20";

    #[test]
    fn part1_works() {
        let result = process_part1(INPUT_1);
        assert_eq!(result, 13);
    }

    #[test]
    fn part2_1_works() {
        let result = process_part2(INPUT_1);
        assert_eq!(result, 1);
    }

    #[test]
    fn part2_2_works() {
        let result = process_part2(INPUT_2);
        assert_eq!(result, 36);
    }
}
