use std::collections::{VecDeque};

use itertools::Itertools;
use nom::{branch::alt, bytes::complete::tag, multi::separated_list1};

pub fn process_part1(input: &str) -> u64 {
    let monkeys = parse_input(input);

    compute_monkey_business_level(20, monkeys, ReliefStrategy::Divide(3))
}

pub fn process_part2(input: &str) -> u64 {
    process_rounds(input, 10_000)
}

pub fn process_rounds(input: &str, rounds: u32) -> u64 {
    let monkeys = parse_input(input);
    let relief_factor = monkeys.iter().map(|f| f.test_factor).product();
    compute_monkey_business_level(rounds, monkeys, ReliefStrategy::Modulo(relief_factor))
}

fn compute_monkey_business_level(rounds: u32, mut monkeys: Vec<Monkey>, relief_strategy: ReliefStrategy) -> u64 {
    for _ in 1..=rounds {
        for monkey_id in 0..monkeys.len() {            
            for _ in 0..monkeys[monkey_id].items.len() {
                let monkey = monkeys.get_mut(monkey_id).unwrap();
                let item = monkey.items.pop_front().unwrap();
                let result = monkey.inspect(item, &relief_strategy);
            
                let target_monkey = monkeys.get_mut(result.target_monkey as usize).unwrap();

                target_monkey.items.push_back(result.new_item);
            }
        }
    }

    monkeys.iter().map(|m| m.inspections as u64).sorted().rev().take(2).product::<u64>()
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
    let (input, items) = separated_list1(tag(", "), nom::character::complete::u64)(input)?;

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

enum ReliefStrategy {
    Divide(u32),
    Modulo(u32),
}

#[derive(Debug, Clone)]
struct Monkey {
    id: u32,
    items: VecDeque<u64>,
    operation: Operation,
    test_factor: u32,
    target_if_true: u32,
    target_if_false: u32,
    inspections: u32,
}

impl Monkey {
    fn inspect(&mut self, item: u64, relief_strategy: &ReliefStrategy) -> InspectionResult {
        let pre_result = match self.operation {
            Operation::Add(operand) => item + operand as u64,
            Operation::MultiplySelf => item * item,
            Operation::MultiplyBy(operand) => item * operand as u64,
        };

        let result = match relief_strategy {
            ReliefStrategy::Divide(factor) => pre_result / *factor as u64,
            ReliefStrategy::Modulo(factor) => pre_result % *factor as u64,
        };

        self.inspections += 1;

        InspectionResult {
            new_item: result,
            target_monkey: if result % self.test_factor as u64 == 0 {
                self.target_if_true
            } else {
                self.target_if_false
            }
        }
    }
}

#[derive(Debug)]
struct InspectionResult {
    new_item: u64,
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
    fn part2_1_works() {
        let result = process_rounds(INPUT, 1);
        
        assert_eq!(result, 24);
    }

    #[test]    
    fn part2_20_works() {
        let result = process_rounds(INPUT, 20);
        
        assert_eq!(result, 99*103);
    }

    #[test]    
    fn part2_1000_works() {
        let result = process_rounds(INPUT, 1000);
        
        assert_eq!(result, 5204*5192);
    }

    #[test]    
    fn part2_2000_works() {
        let result = process_rounds(INPUT, 2000);
        
        assert_eq!(result, 10419*10391);
    }

    #[test]    
    fn part2_3000_works() {
        let result = process_rounds(INPUT, 3000);
        
        assert_eq!(result, 15638*15593);
    }

    #[test]    
    fn part2_4000_works() {
        let result = process_rounds(INPUT, 4000);
        
        assert_eq!(result, 20858*20797);
    }

    #[test]    
    fn part2_5000_works() {
        let result = process_rounds(INPUT, 5000);
        
        assert_eq!(result, 26075*26000);
    }

    #[test]    
    fn part2_6000_works() {
        let result = process_rounds(INPUT, 6000);
        
        assert_eq!(result, 31294*31204);
    }

    #[test]    
    fn part2_7000_works() {
        let result = process_rounds(INPUT, 7000);
        
        assert_eq!(result, 36508*36400);
    }

    #[test]    
    fn part2_8000_works() {
        let result = process_rounds(INPUT, 8000);
        
        assert_eq!(result, 41728*41606);
    }

    #[test]    
    fn part2_9000_works() {
        let result = process_rounds(INPUT, 9000);
        
        assert_eq!(result, 46945*46807);
    }

    #[test]
    fn part2_works() {
        let result = process_part2(INPUT);
        assert_eq!(result, 52166*52013);
    }
}
