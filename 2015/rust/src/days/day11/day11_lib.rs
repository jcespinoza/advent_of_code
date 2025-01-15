#[derive(Debug)]
pub struct Password {
  key: Vec<u8>,
}

impl Password {
  pub fn new(key: Vec<u8>) -> Password {
    Password { key }
  }
}

impl std::fmt::Display for Password {
  fn fmt(&self, f: &mut std::fmt::Formatter<'_>) -> std::fmt::Result {
    write!(
      f,
      "{}",
      self.key.iter().map(|c| *c as char).collect::<String>()
    )
  }
}

impl From<&str> for Password {
  fn from(key: &str) -> Self {
    Password {
      key: key.chars().map(|c| c as u8).collect(),
    }
  }
}

pub fn generate_next_valid_password(old_password: Password) -> String {
  let mut iterations: i64 = 0;
  let mut current_password = old_password;
  while iterations < 1_000_000_000 {
    current_password = compute_next_password(current_password);

    if is_password_valid(&current_password) {
      return current_password.to_string();
    }
    iterations += 1;
  }
  "ERROR".to_string()
}

pub fn is_password_valid(test_password: &Password) -> bool {
  let mut has_straight = false;
  let mut last_char = ' ';
  let mut last_last_char = ' ';
  let mut pairs = Vec::new();

  for c in test_password.key.iter().map(|c| *c as char) {
    // checks for forbiden characters: i, o, l
    if c == 'i' || c == 'o' || c == 'l' {
      return false;
    }

    // checks cc, dd, etc. It will also add one ccc, ddd, etc. to the pairs list
    if c == last_char && c != last_last_char {
      pairs.push(c);
    }

    // remove the last pair if we detect it's a triple
    if c == last_char && c == last_last_char {
      pairs.pop();
    }

    // compare the last three characters to see if they are a straight
    if c as u8 == last_char as u8 + 1 && last_char as u8 == last_last_char as u8 + 1 {
      has_straight = true;
    }

    last_last_char = last_char;
    last_char = c;
  }

  has_straight && pairs.len() >= 2
}

pub fn compute_next_password(old_password: Password) -> Password {
  // iterate over the password bytes in reverse order
  let mut new_password = old_password.key.clone();
  for i in (0..new_password.len()).rev() {
    // increment the current byte
    new_password[i] += 1;

    // if the byte is now past 'z', set it to 'a' and continue to the next byte
    if new_password[i] > b'z' {
      new_password[i] = b'a';
    } else {
      break;
    }
  }

  Password::new(new_password)
}

// unit tests
#[cfg(test)]
mod tests {
  use super::*;

  #[test]
  fn test_is_password_valid() {
    assert!(!is_password_valid(&Password::from("hijklmmn")));
    assert!(!is_password_valid(&Password::from("abbceffg")));
    assert!(!is_password_valid(&Password::from("abbcegjk")));
    assert!(is_password_valid(&Password::from("abcdffaa")));
    assert!(is_password_valid(&Password::from("ghjaabcc")));
  }

  #[test]
  fn test_compute_next_password_01() {
    assert_eq!(
      generate_next_valid_password(Password::from("abcdefgh")),
      "abcdffaa"
    );
  }

  #[test]
  fn test_compute_next_password_02() {
    assert_eq!(
      generate_next_valid_password(Password::from("ghijklmn")),
      "ghjaabcc"
    );
  }
}
