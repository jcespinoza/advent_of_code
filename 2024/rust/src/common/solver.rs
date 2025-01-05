use crate::common::input_strategy::PuzzleInputReaderStrategy;
pub mod solver;

pub trait SteppedSolver<TParsedInputOne, TParsedInputTwo, TResultOne, TResultTwo> {
    fn parse_input_one(&self, input: Vec<String>) -> TParsedInputOne;
    fn parse_input_two(&self, input: Vec<String>) -> TParsedInputTwo;
    fn part_one(&self, input: TParsedInputOne) -> TResultOne;
    fn part_two(&self, input: TParsedInputTwo) -> TResultTwo;
}

pub struct SteppedSolver<TParsedInputOne, TParsedInputTwo, TResultOne, TResultTwo> {
    puzzle_input_reader: Box<dyn PuzzleInputReaderStrategy>,
    puzzle_input_one: Lazy<TParsedInputOne>,
    puzzle_input_two: Lazy<TParsedInputTwo>,
}

impl<TParsedInputOne, TParsedInputTwo, TResultOne, TResultTwo>
    SteppedSolver<TParsedInputOne, TParsedInputTwo, TResultOne, TResultTwo>
where
    TParsedInputOne: 'static,
    TParsedInputTwo: 'static,
{
    pub fn new_local(input_path: String) -> Self {
        let reader = Box::new(LocalPuzzleInputReaderStrategy { input_path });
        Self::new(reader)
    }

    pub fn new_remote(year: i32, day: i32) -> Self {
        let reader = Box::new(RemotePuzzleInputReaderStrategy { year, day });
        Self::new(reader)
    }

    fn new(puzzle_input_reader: Box<dyn PuzzleInputReaderStrategy>) -> Self {
        let puzzle_input_one = Lazy::new(|| {
            let content = puzzle_input_reader.read_input().unwrap();
            Self::parse_input_one(content)
        });

        let puzzle_input_two = Lazy::new(|| {
            let content = puzzle_input_reader.read_input().unwrap();
            Self::parse_input_two(content)
        });

        SteppedSolver {
            puzzle_input_reader,
            puzzle_input_one,
            puzzle_input_two,
        }
    }

    pub fn parse_input_one(input: Vec<String>) -> TParsedInputOne {
        unimplemented!()
    }

    pub fn parse_input_two(input: Vec<String>) -> TParsedInputTwo {
        unimplemented!()
    }

    pub fn part_one(&self, input: TParsedInputOne) -> TResultOne {
        unimplemented!()
    }

    pub fn part_two(&self, input: TParsedInputTwo) -> TResultTwo {
        unimplemented!()
    }
}

pub struct Solver<TInput, TResult> {
    puzzle_input: Lazy<TInput>,
    puzzle_input_reader: Box<dyn PuzzleInputReaderStrategy>,
}

impl<TInput, TResult> Solver<TInput, TResult> {
    pub fn new_with_local_file(input_path: String) -> Self {
        let reader = Box::new(LocalPuzzleInputReaderStrategy { input_path });
        let puzzle_input = Lazy::new(|| {
            let content = reader.read_input().unwrap();
            Self::parse_input(content)
        });
        Self {
            puzzle_input,
            puzzle_input_reader: reader,
        }
    }

    pub fn new_with_remote(year: i32, day: i32) -> Self {
        let reader = Box::new(RemotePuzzleInputReaderStrategy { year, day });
        let puzzle_input = Lazy::new(|| {
            let content = reader.read_input().unwrap();
            Self::parse_input(content)
        });
        Self {
            puzzle_input,
            puzzle_input_reader: reader,
        }
    }

    pub fn parse_input(input: Vec<String>) -> TInput {
        // Implement the logic to parse input
        unimplemented!()
    }

    pub fn part_one(&self, input: TInput) -> TResult {
        // Implement the logic for part one
        unimplemented!()
    }

    pub fn part_two(&self, input: TInput) -> TResult {
        // Implement the logic for part two
        unimplemented!()
    }
}
