use day_x::process_part1;
use std::fs;

fn main() {
  let file = fs::read_to_string("input.txt").expect("Error reading file");
  println!("{}", process_part1(&file));
}