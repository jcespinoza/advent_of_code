pub struct AocExample<'a, TResult> {
  pub expected_output: TResult,
  pub raw_input: Vec<&'a str>,
}
