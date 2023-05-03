use std::{str::FromStr};

const ASCII_UPPER_A: u8 = 65;
const ASCII_LOWER_A: u8 = 97;

#[derive(Debug)]
struct Rucksack {
    compartments: Vec<char>,
    shared_items: char,
}

impl FromStr for Rucksack {
    type Err = String;

    fn from_str(s: &str) -> Result<Self, Self::Err> {
        let compartment1: Vec<char> = s[..s.len() / 2].chars().collect();
        let compartment2: Vec<char> = s[s.len() / 2..].chars().collect();

        assert_eq!(compartment1.len(), compartment2.len());

        Ok(Rucksack {
            shared_items: get_shared_item(&compartment1, &compartment2).unwrap(),
            compartments: s.chars().collect(),
        })
    }
}

fn get_shared_item(rucksack1: &[char], rucksack2: &[char]) -> Result<char, String> {
    for item in rucksack1 {
        if rucksack2.contains(item) {
            return Ok(*item);
        }
    }
    for item in rucksack2 {
        if rucksack1.contains(item) {
            return Ok(*item);
        }
    }
    Err("No shared items".to_string())
}

fn char_to_priority(c: char) -> u32 {
    let ascci_code: u32 = c.into();
    if c.is_lowercase() {
        return ascci_code - u32::from(ASCII_LOWER_A) + 1;
    }
    ascci_code - u32::from(ASCII_UPPER_A) + 27
}

pub fn process_part1(input: &str) -> u32 {
    let lines: Vec<&str> = input.lines().map(str::trim).collect();
    let rucksaks: Vec<Rucksack> = lines
        .iter()
        .map(|s| s.parse::<Rucksack>().unwrap())
        .collect();

    let priorities: Vec<u32> = rucksaks
        .iter()
        .map(|rucksack| char_to_priority(rucksack.shared_items))
        .collect();

    priorities.iter().sum::<u32>()
}

pub fn process_part2(input: &str) -> u32 {
    let lines: Vec<&str> = input.lines().map(str::trim).collect();
    let rucksaks: Vec<Rucksack> = lines
        .iter()
        .map(|s| s.parse::<Rucksack>().unwrap())
        .collect();

    let group_items: Vec<u32> = rucksaks.chunks(3).map(find_common_item).collect();

    group_items.iter().sum::<u32>()
}

fn find_common_item(rucksaks: &[Rucksack]) -> u32 {
    assert!(rucksaks.len() == 3);

    let a_rucksack = &rucksaks[0];
    let b_rucksack = &rucksaks[1];
    let c_rucksack = &rucksaks[2];

    let common_char = a_rucksack
        .compartments
        .iter()
        .find(|target_c| {
            b_rucksack.compartments.contains(target_c) && c_rucksack.compartments.contains(target_c)
        })
        .unwrap();

    char_to_priority(*common_char)
}

#[cfg(test)]
mod tests {
    use super::*;
    const INPUT: &str = "vJrwpWtwJgWrhcsFMMfFFhFp
    jqHRNqRjqzjGDLGLrsFMfFZSrLrFZsSL
    PmmdzqPrVvPwwTWBwg
    wMqvLMZHhHMvwLHjbvcjnnSBnvTQFn
    ttgJtRGJQctTZtZT
    CrZsJsPPZsGzwwsLwLmpwMDw";

    #[test]
    fn part1_works() {
        // Rucksacks have two compartments with equal number of items in each.

        let result = process_part1(INPUT);
        assert_eq!(result, 157);
    }

    #[test]
    fn part2_works() {
        let result = process_part2(INPUT);
        assert_eq!(result, 70);
    }
}
