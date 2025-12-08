/// A struct to hold a problem set consisting a grid of numbers while the last row indicates operations.
/// The last row contains either '*' for multiplication or '+' for addition.
#[derive(Debug, Clone)]
pub struct CephaloProblemSet {
  pub problems: Vec<Problem>,
}

#[derive(Debug, Clone)]
pub struct Problem {
  pub operation: Operation,
  pub values: Vec<i64>,
}

/// Enum to represent the operations in the last row of the problem set.
#[derive(Debug, Clone, PartialEq)]
pub enum Operation {
  Multiply,
  Add,
}

impl From<&str> for Operation {
  fn from(op_str: &str) -> Self {
    match op_str {
      "*" => Operation::Multiply,
      "+" => Operation::Add,
      _ => panic!("Invalid operation string: {}", op_str),
    }
  }
}

impl CephaloProblemSet {
  pub fn from(input: Vec<&str>, read_as_cephalopod: bool) -> Self {
    let mut problems = Vec::new();

    if read_as_cephalopod {
      // If the input is in cephalopod form, parse accordingly
      // (Assuming cephalopod form is different; implement as needed)
      cephalopod_form_read(input, &mut problems);
    } else {
      // Human-readable form parsing
      human_form_read(input, &mut problems);
    }

    CephaloProblemSet { problems }
  }
}

fn human_form_read(input: Vec<&str>, problems: &mut Vec<Problem>) {
  let parts: Vec<&str> = input[0].split_whitespace().collect();
  problems.resize_with(parts.len(), || Problem {
    operation: Operation::Add, // default, will be overwritten
    values: Vec::new(),
  });

  for (index, line) in input.iter().enumerate() {
    let parts: Vec<&str> = line.split_whitespace().collect();
    if index == input.len() - 1 {
      // Last row contains operations
      for (index, part) in parts.iter().enumerate() {
        problems[index].operation = Operation::from(*part);
      }
    } else {
      // Other rows contain numbers
      for (index, number) in parts.iter().enumerate() {
        let value = number.parse::<i64>().expect("Invalid number in grid");
        problems[index].values.push(value);
      }
    }
  }
}

fn cephalopod_form_read(input: Vec<&str>, problems: &mut Vec<Problem>) {
  let parts: Vec<&str> = input[0].split_whitespace().collect();
  problems.resize_with(parts.len(), || Problem {
    operation: Operation::Add, // default, will be overwritten
    values: Vec::new(),
  });

  // Get the start and end indices for each column.
  // The start index is where the operation symbol lies in the last row.
  // The end of the column is 1 minus the location of the operation symbol of the next column (or the end of the line for the last column).
  // The start and end indices should never be the same
  let last_row = input.len() - 1;
  let mut col_start_end_indices: Vec<(usize, usize)> = Vec::new();
  let last_line = input[last_row];

  let mut current_start: Option<usize> = None;
  for (idx, ch) in last_line.chars().enumerate() {
    if ch == '*' || ch == '+' {
      // Found an operation symbol
      if let Some(start) = current_start {
        // Close off the previous column
        col_start_end_indices.push((start, idx - 1));
      }
      current_start = Some(idx);
    }
  }
  // Add the last column
  if let Some(start) = current_start {
    col_start_end_indices.push((start, last_line.len()));
  }

  // Now create the list of problems based on the column indices
  problems.resize(
    col_start_end_indices.len(),
    Problem {
      operation: Operation::Add,
      values: [].to_vec(),
    },
  );

  for (col_idx, (start, end)) in col_start_end_indices.iter().enumerate() {
    // Get the operation from the last row
    let op_char = last_line.chars().nth(*start).unwrap();

    problems[col_idx].operation = Operation::from(op_char.to_string().as_str());

    for char_index in *start..*end {
      // keep track of the digits that we find at the current char_index in all rows
      let mut number_str = String::new();
      for row_idx in 0..last_row {
        let line = input[row_idx];
        // add it to the number string if it's not a space
        if line.chars().nth(char_index).unwrap() != ' ' {
          number_str.push(line.chars().nth(char_index).unwrap());
        }
      }
      // Build the actual number from the collected digits
      let value = number_str.trim().parse::<i64>().unwrap();
      problems[col_idx].values.push(value);
    }
  }
}
/// Computes the answers for each column in the CephaloProblemSet based on the specified operations.
pub fn compute_answers(input: CephaloProblemSet) -> Vec<i64> {
  let mut results: Vec<i64> = Vec::new();

  for problem in &input.problems {
    let col_values = &problem.values;
    let col_result = match &problem.operation {
      Operation::Add => col_values.iter().sum(),
      Operation::Multiply => col_values.iter().product(),
    };
    results.push(col_result);
  }

  results
}
