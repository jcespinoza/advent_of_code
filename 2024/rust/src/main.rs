use advent_of_code::common::{input_strategy::PuzzleInputProvider, solver::create_solver};

// receive a year, day number and part number as arguments
// program should be executed as `cargo run --year 2024 --day 3 --part 1`
// or cargo run -y 2024 -d 3 -p 1
// or advent_of_code --year 2024 --day 3 --part 1
// or advent_of_code -y 2024 -d 3 -p 1
fn main() {
  // Extract the year and number from the arguments
  // Show an error message if the arguments are not provided
  let args: Vec<String> = std::env::args().collect();
  if args.len() < 7 {
    eprintln!("Usage: advent_of_code --year <year> --day <day> --part <part>");
    std::process::exit(1);
  }

  let mut year = 0;
  let mut day = 0;
  let mut part = 0;

  for i in 0..args.len() {
    if args[i] == "--year" || args[i] == "-y" {
      year = args[i + 1].parse::<i32>().unwrap();
    } else if args[i] == "--day" || args[i] == "-d" {
      day = args[i + 1].parse::<i32>().unwrap();
    } else if args[i] == "--part" || args[i] == "-p" {
      part = args[i + 1].parse::<i32>().unwrap();
    }
  }

  // Make sure part is either 1 or 2
  if part != 1 && part != 2 {
    eprintln!("Part must be either 1 or 2");
    std::process::exit(1);
  }

  // Create a new solver for the given year and day
  let input_provider = PuzzleInputProvider::new_remote(year, day);
  let input = input_provider.read_input().unwrap();

  let solver = create_solver(year, day);
  let result = match part {
    1 => solver.solve_part_one_str(input.iter().map(|x| x.as_str()).collect()),
    2 => solver.solve_part_two_str(input.iter().map(|x| x.as_str()).collect()),
    _ => panic!("Invalid part number"),
  };

  println!("{}", result);
}
