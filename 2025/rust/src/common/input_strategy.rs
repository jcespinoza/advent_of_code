use reqwest::{header::COOKIE, Client};
use tokio::runtime::Runtime;

use std::{
  env,
  fs::{self, File},
  io::{self, BufRead},
  path::Path,
};

/// PuzzleInputReaderStrategy defines the interface for reading puzzle inputs
/// from different sources.
pub trait PuzzleInputReaderStrategy {
  fn read_input(&self) -> io::Result<Vec<String>>;
}

/// LocalPuzzleInputReaderStrategy reads puzzle input from a local file.
/// The file path is provided during the struct initialization.
/// It reads the file line by line and returns a vector of strings.
pub struct LocalPuzzleInputReaderStrategy {
  pub input_path: String,
}

impl PuzzleInputReaderStrategy for LocalPuzzleInputReaderStrategy {
  fn read_input(&self) -> io::Result<Vec<String>> {
    let path = Path::new(&self.input_path);
    let file = File::open(path)?;
    let reader = io::BufReader::new(file);
    reader.lines().collect()
  }
}

/// RemotePuzzleInputReaderStrategy fetches puzzle input from the Advent of Code website.
/// It requires an environment variable `AOC_COOKIE` to authenticate the request.
/// It also caches the input locally in the `inputs/puzzle_inputs` directory
/// to avoid redundant network requests.
pub struct RemotePuzzleInputReaderStrategy {
  pub year: i32,
  pub day: i32,
}

impl RemotePuzzleInputReaderStrategy {
  const ADVENT_OF_CODE_BASE_URL: &'static str = "https://adventofcode.com";

  fn get_cookie_value() -> Result<String, io::Error> {
    env::var("AOC_COOKIE").map_err(|_| {
      io::Error::other("You need to specify your cookie in order to get your input puzzle")
    })
  }

  fn get_client() -> Result<Client, io::Error> {
    let cookie_value = Self::get_cookie_value()?;
    let unwrapped_cookie = format!("session={}", cookie_value);
    let client = Client::builder()
      .default_headers({
        let mut headers = reqwest::header::HeaderMap::new();
        headers.insert(COOKIE, unwrapped_cookie.parse().unwrap());
        headers
      })
      .build()
      .map_err(|e| io::Error::other(e.to_string()))?;
    Ok(client)
  }

  pub async fn read_input(&self) -> Result<Vec<String>, io::Error> {
    dotenv::dotenv().ok();
    if self.day <= 0 {
      return Err(io::Error::new(
        io::ErrorKind::InvalidInput,
        "Day must be a positive integer",
      ));
    }

    let inputs_path = env::var("AOC_INPUT_PATH").unwrap_or_else(|_| "inputs".to_string());
    let file_path = format!(
      "{}/puzzle_inputs/{}_{}.txt",
      inputs_path, self.year, self.day
    );

    if Path::new(&file_path).exists() {
      let file = fs::File::open(file_path)?;
      let reader = io::BufReader::new(file);
      return reader.lines().collect();
    }

    let client = Self::get_client()?;
    let url = format!(
      "{}/{}/day/{}/input",
      Self::ADVENT_OF_CODE_BASE_URL,
      self.year,
      self.day
    );
    let response = client
      .get(&url)
      .send()
      .await
      .map_err(|e| io::Error::other(e.to_string()))?;
    let string_result = response
      .text()
      .await
      .map_err(|e| io::Error::other(e.to_string()))?;

    // If the response starts with the text "Puzzle inputs differ by user", throw an error
    if string_result.starts_with("Puzzle inputs differ by user") {
      return Err(io::Error::other(
        "Unable to fetch the puzzle input. Please make sure you have the correct cookie",
      ));
    }

    if !string_result.trim().is_empty() {
      fs::create_dir_all(format!("{}/puzzle_inputs", inputs_path))?;
      fs::write(&file_path, &string_result)?;
    }

    Ok(string_result.lines().map(|s| s.to_string()).collect())
  }
}

impl PuzzleInputReaderStrategy for RemotePuzzleInputReaderStrategy {
  fn read_input(&self) -> io::Result<Vec<String>> {
    let rt = Runtime::new().unwrap();
    rt.block_on(self.read_input())
  }
}

/// The PuzzleInputProvider struct uses a strategy pattern to read puzzle inputs
/// from different sources (local file or remote server).
/// It delegates the reading operation to the selected strategy.
pub struct PuzzleInputProvider {
  puzzle_input_reader: Box<dyn PuzzleInputReaderStrategy>,
}

impl PuzzleInputProvider {
  /// Creates a new PuzzleInputProvider that reads input from a local file.
  pub fn new_local(input_path: String) -> Self {
    let reader = Box::new(LocalPuzzleInputReaderStrategy { input_path });
    Self::new(reader)
  }

  /// Creates a new PuzzleInputProvider that reads input from the Advent of Code website.
  pub fn new_remote(year: i32, day: i32) -> Self {
    let reader = Box::new(RemotePuzzleInputReaderStrategy { year, day });
    Self::new(reader)
  }

  /// Internal constructor to initialize the PuzzleInputProvider with a given strategy.
  fn new(puzzle_input_reader: Box<dyn PuzzleInputReaderStrategy>) -> Self {
    PuzzleInputProvider {
      puzzle_input_reader,
    }
  }

  /// Reads the puzzle input using the selected strategy.
  pub fn read_input(&self) -> io::Result<Vec<String>> {
    self.puzzle_input_reader.read_input()
  }
}
