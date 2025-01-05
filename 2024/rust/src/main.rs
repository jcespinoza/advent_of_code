use advent_of_code::common::RemotePuzzleInputReaderStrategy;

#[tokio::main]
async fn main() {
    // Use the RemotePuzzleInputReaderStrategy struct to fetch the input from the Advent of Code website
    // and print it to the console
    let strategy = RemotePuzzleInputReaderStrategy { year: 2024, day: 1 };
    println!("Fetching input...");
    let input = strategy.read_input().await.unwrap();
    println!("{:?}", input);
}
