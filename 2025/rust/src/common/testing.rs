/// A structure representing an example input and its expected output for Advent of Code problems.
/// 'a is the lifetime of the raw input strings, and TResult is the type of the expected output.
pub struct AocExample<'a, TResult> {
  pub expected_output: TResult,
  pub raw_input: Vec<&'a str>,
}
