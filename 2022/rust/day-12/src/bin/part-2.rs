use day_x::process_part2;
use std::fs;

fn main() {
  let file = fs::read_to_string("input.txt").expect("Error reading file");
  println!("{}", process_part2(&file));
}