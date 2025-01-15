use advent_of_code::{
  common::{PuzzleInputProvider, YEAR_NUM},
  integration::solver_mapping::create_solver,
};

fn main() {
  let args: Vec<String> = std::env::args().collect();
  if args.len() < 5 {
    eprintln!("Usage: advent_of_code --day <day> --part <part> [--input <input>]");
    std::process::exit(1);
  }

  let mut day = 0;
  let mut part = 0;
  let mut inline_input: &str = "";
  let mut inline_input_provided = false;

  for i in 0..args.len() {
    if args[i] == "--day" || args[i] == "-d" {
      day = args[i + 1].parse::<i32>().unwrap();
    } else if args[i] == "--part" || args[i] == "-p" {
      part = args[i + 1].parse::<i32>().unwrap();
    } else if args[i] == "--input" || args[i] == "-i" {
      println!("Input: {}", args[i + 1]);
      inline_input_provided = true;
      inline_input = &args[i + 1];
    }
  }

  if part != 1 && part != 2 {
    eprintln!("Part must be either 1 or 2");
    std::process::exit(1);
  }

  let mut puzzle_input = vec![];
  if inline_input_provided {
    puzzle_input.push(inline_input.to_string());
  } else {
    let input_provider = PuzzleInputProvider::new_remote(YEAR_NUM, day);
    puzzle_input = input_provider.read_input().unwrap();
  }

  let solver = create_solver(YEAR_NUM, day);
  let result = match part {
    1 => solver.solve_part_one_str(puzzle_input.iter().map(|x| x.as_str()).collect()),
    2 => solver.solve_part_two_str(puzzle_input.iter().map(|x| x.as_str()).collect()),
    _ => panic!("Invalid part number"),
  };

  println!("{}", result);
}
