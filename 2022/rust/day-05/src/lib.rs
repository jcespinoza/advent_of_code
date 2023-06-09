use std::{collections::HashMap};

use itertools::Itertools;
use nom::{
    bytes::complete::tag, character::complete, multi::separated_list1,
    IResult,
};

pub fn process_part1(input: &str) -> String {
    let (moves, mut warehouse) = parse_warehouse(input);

    for step in moves {
        warehouse.move_crates_9000(&step);
    }

    warehouse.get_top_crates().into_iter().collect()
}

pub fn process_part2(input: &str) -> String {
    let (moves, mut warehouse) = parse_warehouse(input);

    for step in moves {
        warehouse.move_crates_9001(&step);
    }

    warehouse.get_top_crates().into_iter().collect()
}

fn parse_warehouse(input: &str) -> (Vec<MoveProcedure>, Warehouse) {
    let split = &mut input.split("\n\n");
    let crate_state_lines = split.next().unwrap().lines().collect::<Vec<_>>();

    let n = crate_state_lines.len();
    let crate_position_lines = &crate_state_lines[..n - 1];
    let crate_names_line = crate_state_lines.last().unwrap().trim();

    let mut crate_hash: HashMap<u32, CrateStack> = HashMap::new();
    let (_, crate_numbers) = separated_list1(tag("   "), an_integer)(crate_names_line).unwrap();

    for name in crate_numbers {
        crate_hash.insert(
            name,
            CrateStack {
                name,
                crates: vec![],
            },
        );
    }
    for line in crate_position_lines {
        for (i, chunk) in line.chars().collect::<Vec<char>>().chunks(4).enumerate() {
            let chunk_as_str = &chunk.iter().collect::<String>();
            let trimmed_chunk = chunk_as_str.trim();
            if trimmed_chunk.len() == 0 {
                continue;
            }

            let (_, crate_obj) = crate_item(trimmed_chunk).unwrap();
            let stack = crate_hash.get_mut(&((i+1) as u32)).unwrap();
            stack.push(crate_obj);
        }
    }
    for stack in crate_hash.values_mut() {
        stack.reverse();
    }

    let movements_instructions = split.next().unwrap();
    let (_, moves) =
        separated_list1(complete::newline, move_procedure)(movements_instructions).unwrap();

    let mut warehouse = Warehouse {
        stacks: crate_hash,
    };
    (moves, warehouse)
}

fn an_integer(input: &str) -> IResult<&str, u32> {
    let (input, num) = complete::u32(input)?;
    Ok((input, num))
}

fn move_procedure(input: &str) -> IResult<&str, MoveProcedure> {
    let (input, _) = tag("move ")(input)?;
    let (input, qty) = complete::u32(input)?;
    let (input, _) = tag(" from ")(input)?;
    let (input, from) = complete::u32(input)?;
    let (input, _) = tag(" to ")(input)?;
    let (input, to) = complete::u32(input)?;

    Ok((input, MoveProcedure { qty, from, to }))
}

fn crate_item(input: &str) -> IResult<&str, char> {
    let (input, _) = tag("[")(input)?;
    let (input, item) = nom::character::complete::anychar(input)?;
    let (input, _) = tag("]")(input)?;
    Ok((input, item))
}

#[derive(Debug)]
struct CrateStack {
    name: u32,
    crates: Vec<char>,
}

impl CrateStack {
    fn pop(&mut self) -> Option<char> {
        self.crates.pop()
    }
    fn push(&mut self, item: char) {
        self.crates.push(item);
    }
    fn push_n(&mut self, items: Vec<char>) {
        for item in items {
            self.push(item);
        }
    }
    fn peek(&self) -> Option<&char> {
        self.crates.last()
    }
    fn reverse(&mut self) {
        self.crates.reverse();
    }
    fn pop_many(&mut self, n: usize) -> Vec<char> {
        self.crates.drain((self.crates.len()-n)..).collect()
    }
}

#[derive(Debug)]
struct Warehouse {
    stacks: HashMap<u32, CrateStack>,
}

impl Warehouse {
    fn move_crates_9000(&mut self, move_procedure: &MoveProcedure) {
        for _i in 0..move_procedure.qty{
            let from_stack = self.stacks.get_mut(&move_procedure.from).unwrap();
            let item = from_stack.pop().unwrap();
            let to_stack = self.stacks.get_mut(&move_procedure.to).unwrap();
            to_stack.push(item);
        }
    }

    fn move_crates_9001(&mut self, move_procedure: &MoveProcedure) {
        let qty = move_procedure.qty;
        let from_stack = self.stacks.get_mut(&move_procedure.from).unwrap();
        let items = from_stack.pop_many(qty as usize);
        let to_stack = self.stacks.get_mut(&move_procedure.to).unwrap();
        to_stack.push_n(items);
    }

    fn get_top_crates(&self) -> Vec<char> {
        let mut top_crates = vec![];
        for k in self.stacks.keys().sorted() {
            top_crates.push(self.stacks.get(k).unwrap().peek().unwrap().clone());
        }
        top_crates
    }
}


#[derive(Debug)]
struct MoveProcedure {
    qty: u32,
    from: u32,
    to: u32,
}

#[cfg(test)]
mod tests {
    use super::*;
    const INPUT: &str = "    [D]    
[N] [C]    
[Z] [M] [P]
 1   2   3 

move 1 from 2 to 1
move 3 from 1 to 3
move 2 from 2 to 1
move 1 from 1 to 2";

    #[test]
    fn part1_works() {
        let result = process_part1(INPUT);
        assert_eq!(result, "CMZ");
    }

    #[test]
    fn part2_works() {
        let result = process_part2(INPUT);
        assert_eq!(result, "MCD");
    }
}
