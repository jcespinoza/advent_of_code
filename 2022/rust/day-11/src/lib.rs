use std::collections::{VecDeque};

use itertools::Itertools;
use nom::{branch::alt, bytes::complete::tag, multi::separated_list1};

pub fn process_part1(input: &str) -> u32 {
    let mut monkeys = parse_input(input);

    for _ in 1..=20 {
        for monkey_id in 0..monkeys.len() {            
            for _ in 0..monkeys[monkey_id].items.len() {
                let monkey = monkeys.get_mut(monkey_id).unwrap();
                let item = monkey.items.pop_front().unwrap();
                let result = monkey.inspect(item);
                
                let target_monkey = monkeys.get_mut(result.target_monkey as usize).unwrap();

                target_monkey.items.push_back(result.new_item);
            }
        }
    }

    let top_2_inspections: Vec<u32> = monkeys.iter().map(|m| m.inspections).sorted().rev().take(2).collect();
    assert!(top_2_inspections.len() == 2);

    top_2_inspections[0] * top_2_inspections[1]
}

pub fn process_part2(input: &str) -> u32 {
    0
}

fn operation_parser(input: &str) -> nom::IResult<&str, Operation> {
    let (input, _) = tag("old ")(input)?;
    let (input, operator) = alt((tag("+"), tag("*")))(input)?;
    let (input, _) = tag(" ")(input)?;
    let (input, operand) = alt((tag("old"), nom::character::complete::digit1))(input)?;

    let operation = match operator {
        "+" => Operation::Add(operand.parse().unwrap()),
        _ => match operand {
            "old" => Operation::MultiplySelf,
            _ => Operation::MultiplyBy(operand.parse().unwrap()),
        },
    };

    Ok((input, operation))
}

fn monkey_parser(input: &str) -> nom::IResult<&str, Monkey> {
    let (input, _) = tag("Monkey ")(input)?;
    let (input, id) = nom::character::complete::u32(input)?;
    let (input, _) = tag(":")(input)?;
    let (input, _) = tag("\n  Starting items: ")(input)?;
    let (input, items) = separated_list1(tag(", "), nom::character::complete::u32)(input)?;

    let (input, _) = tag("\n  Operation: new = ")(input)?;
    let (input, operation) = operation_parser(input)?;

    let (input, _) = tag("\n  Test: divisible by ")(input)?;
    let (input, test_factor) = nom::character::complete::u32(input)?;
    let (input, _) = tag("\n    If true: throw to monkey ")(input)?;
    let (input, target_if_true) = nom::character::complete::u32(input)?;
    let (input, _) = tag("\n    If false: throw to monkey ")(input)?;
    let (input, target_if_false) = nom::character::complete::u32(input)?;

    Ok((
        input,
        Monkey {
            id,
            items: VecDeque::from(items),
            operation,
            test_factor,
            target_if_true,
            target_if_false,
            inspections: 0,
        },
    ))
}

fn parse_input(input: &str) -> Vec<Monkey> {
    let (_, monkeys) = separated_list1(tag("\n\n"), monkey_parser)(input).unwrap();

    monkeys
}

#[derive(Debug, Clone)]
struct Monkey {
    id: u32,
    items: VecDeque<u32>,
    operation: Operation,
    test_factor: u32,
    target_if_true: u32,
    target_if_false: u32,
    inspections: u32,
}

impl Monkey {
    fn inspect(&mut self, item: u32) -> InspectionResult {
        let result = match self.operation {
            Operation::Add(operand) => item + operand,
            Operation::MultiplySelf => item * item,
            Operation::MultiplyBy(operand) => item * operand,
        } / 3;

        self.inspections += 1;

        InspectionResult {
            new_item: result,
            target_monkey: if result % self.test_factor == 0 {
                self.target_if_true
            } else {
                self.target_if_false
            }
        }
    }
}

#[derive(Debug)]
struct InspectionResult {
    new_item: u32,
    target_monkey: u32,
}

#[derive(Debug, Clone)]
enum Operation {
    Add(u32),
    MultiplySelf,
    MultiplyBy(u32),
}

#[cfg(test)]
mod tests {
    use super::*;
    const INPUT: &str = "Monkey 0:
  Starting items: 79, 98
  Operation: new = old * 19
  Test: divisible by 23
    If true: throw to monkey 2
    If false: throw to monkey 3

Monkey 1:
  Starting items: 54, 65, 75, 74
  Operation: new = old + 6
  Test: divisible by 19
    If true: throw to monkey 2
    If false: throw to monkey 0

Monkey 2:
  Starting items: 79, 60, 97
  Operation: new = old * old
  Test: divisible by 13
    If true: throw to monkey 1
    If false: throw to monkey 3

Monkey 3:
  Starting items: 74
  Operation: new = old + 3
  Test: divisible by 17
    If true: throw to monkey 0
    If false: throw to monkey 1";

    #[test]
    fn part1_works() {
        let result = process_part1(INPUT);
        assert_eq!(result, 10605);
    }

    #[test]
    #[ignore]
    fn part2_works() {
        let result = process_part2(INPUT);
        assert_eq!(result, 45000);
    }
}
