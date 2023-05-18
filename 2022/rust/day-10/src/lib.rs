use nom::{sequence::separated_pair, branch::alt, IResult, bytes::complete::tag, character::complete::{self, newline}, multi::separated_list1};

pub fn process_part1(input: &str) -> u32 {
    let (_, instructions) = parse_instructions(input).unwrap();
    
    
  0
}

pub fn process_part2(input: &str) -> u32 {
  0
}

pub fn get_signal_during_cycle(instructions: &[Instruction], cycle: u32) -> i32 {
  if cycle <= 1 { return 1; }

  let instructions_subset = instructions.iter().take((cycle-1) as usize).collect::<Vec<&Instruction>>();
  let last_instruction = instructions_subset.last().unwrap();

  let is_last_instruction_addx = match **last_instruction { 
    Instruction::Addx(_) => true,
    _ => false,
  };
  
  let addx_values = instructions_subset.iter().filter_map(|f| match f {
    Instruction::Noop => None,
    Instruction::Addx(value) => Some(value),
  });

  
  let mut signal_strength = 1 + addx_values.sum::<i32>();

  let instructions_taken = instructions_subset.len() as u32;
  let are_there_enough_instructions =  (cycle as i32 - instructions_taken as i32).abs() > 1;
  if are_there_enough_instructions && is_last_instruction_addx {
    signal_strength -= match last_instruction { Instruction::Addx(v) => v, _ => &0 }; 
  }

  signal_strength
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
  fn x_starts_during_1(){
    let (_,instructions) = parse_instructions(INPUT_1).unwrap();
    let signal = get_signal_during_cycle(&instructions, 1);
    assert_eq!(signal, 1);
  }

  #[test]
  fn x_does_not_change_during_cycle_2(){
    let (_,instructions) = parse_instructions(INPUT_1).unwrap();
    let signal = get_signal_during_cycle(&instructions, 2);
    assert_eq!(signal, 1);
  }

  #[test]
  fn x_does_not_change_during_cycle_3(){
    let (_,instructions) = parse_instructions(INPUT_1).unwrap();
    let signal = get_signal_during_cycle(&instructions, 3);
    assert_eq!(signal, 1);
  }
  
  #[test]
  fn x_changes_after_cycle_4(){
    let (_,instructions) = parse_instructions(INPUT_1).unwrap();
    let signal = get_signal_during_cycle(&instructions, 4);
    assert_eq!(signal, 4);
  }
  
  #[test]
  fn x_does_not_change_during_cycle_5(){
    let (_,instructions) = parse_instructions(INPUT_1).unwrap();
    let signal = get_signal_during_cycle(&instructions, 5);
    assert_eq!(signal, 4);
  }
  
  #[test]
  fn x_changes_after_cycle_5(){
    let (_,instructions) = parse_instructions(INPUT_1).unwrap();
    let signal = get_signal_during_cycle(&instructions, 6);
    assert_eq!(signal, -1);
  }

  #[test]
  #[ignore]
  fn part1_1_works() {
    let result = process_part1(INPUT_1);
    assert_eq!(result, 24000);
  }

  #[test]
  #[ignore]  
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