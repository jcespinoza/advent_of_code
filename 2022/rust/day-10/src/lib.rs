use std::collections::BTreeMap;

use itertools::Itertools;
use nom::{
    branch::alt,
    bytes::complete::tag,
    character::complete::{self, newline},
    multi::separated_list1,
    sequence::separated_pair,
    IResult,
};

pub fn process_part1(input: &str) -> u32 {
    let (_, instructions) = parse_instructions(input).unwrap();

    let x_values = get_register_x_at_cycle(instructions);

    println!("{:?}", x_values);
    x_values.iter().sum::<i32>() as u32
}

pub fn process_part2(input: &str) -> String {
    let (_, instructions) = parse_instructions(input).unwrap();
    let mut x_value: i32 = 1;
    let mut cycle_count: u32 = 0;
    let mut crt_pixels: String = "".to_string();

    for instruction in instructions.iter() {
        for duration in 0..instruction.duration() {
            let pixel_id = (cycle_count as i32 + duration as i32) % 40;

            if ((x_value - 1)..=(x_value + 1)).contains(&pixel_id) {
                crt_pixels.push_str("#");
            } else {
                crt_pixels.push_str(".");
            }            
        }

        cycle_count += instruction.duration();

        match instruction {
            Instruction::Noop => {},
            Instruction::Addx(value) => {
                x_value += value;
            }
        };
    }

    crt_pixels
        .chars()
        .chunks(40)
        .into_iter()
        .map(|chunk| chunk.collect::<String>())
        .join("\n")
}

pub fn get_register_x_at_cycle(instructions: Vec<Instruction>) -> Vec<i32> {
    let mut value_of_register_x = 1;
    let mut x_values: BTreeMap<u32, i32> = BTreeMap::new();

    let mut current_cycle = 1;

    for instruction in &instructions {
        if ((current_cycle + 1) as i32 - 20) % 40 == 0 {
            x_values.insert(
                current_cycle + 1,
                value_of_register_x * ((current_cycle + 1) as i32),
            );
        }

        current_cycle += instruction.duration();

        match instruction {
            Instruction::Noop => {}
            Instruction::Addx(value) => {
                value_of_register_x += value;
            }
        }

        if (current_cycle as i32 - 20) % 40 == 0 {
            x_values.insert(
                current_cycle as u32,
                value_of_register_x * (current_cycle as i32),
            );
        }
    }

    x_values.values().map(|v| *v).collect()
}

fn parse_instructions(input: &str) -> IResult<&str, Vec<Instruction>> {
    let (input, instructions) = separated_list1(newline, alt((parse_noop, parse_addx)))(input)?;
    Ok((input, instructions))
}

fn parse_addx(input: &str) -> IResult<&str, Instruction> {
    let (input, (_, value)) = separated_pair(tag("addx"), tag(" "), complete::i32)(input)?;
    Ok((input, Instruction::Addx(value)))
}

fn parse_noop(input: &str) -> IResult<&str, Instruction> {
    let (input, _) = tag("noop")(input)?;
    Ok((input, Instruction::Noop))
}

#[derive(Debug)]
pub enum Instruction {
    Noop,
    Addx(i32),
}

impl Instruction {
    fn duration(&self) -> u32 {
        match self {
            Instruction::Noop => 1,
            Instruction::Addx(_) => 2,
        }
    }
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

    const OUTPUT: &str = "##..##..##..##..##..##..##..##..##..##..
###...###...###...###...###...###...###.
####....####....####....####....####....
#####.....#####.....#####.....#####.....
######......######......######......####
#######.......#######.......#######.....";

    #[test]
    // #[ignore]
    fn part1_2_works() {
        let result = process_part1(INPUT_2);
        assert_eq!(result, 13140);
    }

    #[test]
    // #[ignore]
    fn part2_2_works() {
        let result = process_part2(INPUT_2);
        println!("{}", result);
        assert_eq!(result, OUTPUT);
    }
}
