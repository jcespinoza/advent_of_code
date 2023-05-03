use std::str::FromStr;

pub fn process_part1(input: &str) -> u32 {
    let rounds = input.lines();
    let scores: Vec<_> = rounds.map(|round| {
        let mut elf_moves = round.split_whitespace();
        let opponent_move = elf_moves.next().unwrap().parse::<ElfMove>().unwrap();
        let our_move = elf_moves.next().unwrap().parse::<ElfMove>().unwrap();

        let move_score = match our_move {
          ElfMove::Rock => 1,
          ElfMove::Paper => 2,
          ElfMove::Scissors => 3
        };
        let battle_score = match battle(&our_move, &opponent_move) {
            BattleResult::Win => 6,
            BattleResult::Draw => 3,
            BattleResult::Lose => 0
        };
        move_score + battle_score
    }).collect();

    let sum_of_scores = scores.iter().sum::<u32>();

    sum_of_scores
}

pub fn process_part2(input: &str) -> u32 {
  let rounds = input.lines();
  let scores: Vec<_> = rounds.map(|round| {
      let mut elf_moves = round.split_whitespace();
      let opponent_move = elf_moves.next().unwrap().parse::<ElfMove>().unwrap();
      let desired_outcome = elf_moves.next().unwrap().parse::<BattleResult>().unwrap();

      let our_move = get_strategic_move(&opponent_move, &desired_outcome);

      let move_score = match our_move {
        ElfMove::Rock => 1,
        ElfMove::Paper => 2,
        ElfMove::Scissors => 3
      };
      let battle_score = match desired_outcome {
          BattleResult::Win => 6,
          BattleResult::Draw => 3,
          BattleResult::Lose => 0
      };
      move_score + battle_score
  }).collect();

  let sum_of_scores = scores.iter().sum::<u32>();

  sum_of_scores
}

fn get_strategic_move(opponent_move: &ElfMove, desired_outcome: &BattleResult) -> ElfMove {
    match  (opponent_move, desired_outcome) {
        (ElfMove::Rock, BattleResult::Win) => ElfMove::Paper,
        (ElfMove::Rock, BattleResult::Lose) => ElfMove::Scissors,
        (ElfMove::Paper, BattleResult::Win) => ElfMove::Scissors,
        (ElfMove::Paper, BattleResult::Lose) => ElfMove::Rock,
        (ElfMove::Scissors, BattleResult::Win) => ElfMove::Rock,
        (ElfMove::Scissors, BattleResult::Lose) => ElfMove::Paper,
        (_, BattleResult::Draw) => opponent_move.clone()
    }
}

#[derive(Debug, PartialEq, Clone)]
pub enum ElfMove {
  Rock,
  Paper,
  Scissors,
}

impl FromStr for ElfMove {
  type Err = String;
  fn from_str(s: &str) -> Result<Self, Self::Err> {
    match s {
      "A"|"X" => Ok(ElfMove::Rock),
      "B"|"Y" => Ok(ElfMove::Paper),
      "C"|"Z" => Ok(ElfMove::Scissors),
      _ => Err("Not a known weapon".to_string()),
    }
  }
}

#[derive(Debug, PartialEq)]
pub enum BattleResult {
  Win,
  Lose,
  Draw,
}

impl FromStr for BattleResult {
    type Err = String;
    fn from_str(s: &str) -> Result<Self, Self::Err> {
        match s {
          "X" => Ok(BattleResult::Lose),
          "Y" => Ok(BattleResult::Draw),
          "Z" => Ok(BattleResult::Win),
            _ => Err("Not a known result".to_string()),
        }
    }
}

pub fn battle(elf1: &ElfMove, elf2: &ElfMove) -> BattleResult {
    match (elf1, elf2) {
        (ElfMove::Rock, ElfMove::Paper) => BattleResult::Lose,
        (ElfMove::Rock, ElfMove::Scissors) => BattleResult::Win,
        (ElfMove::Paper, ElfMove::Rock) => BattleResult::Win,
        (ElfMove::Paper, ElfMove::Scissors) => BattleResult::Lose,
        (ElfMove::Scissors, ElfMove::Rock) => BattleResult::Lose,
        (ElfMove::Scissors, ElfMove::Paper) => BattleResult::Win,
        _ => BattleResult::Draw,
        }
}

#[cfg(test)]
mod tests {
    use super::*;
    const INPUT: &str = "A Y
    B X
    C Z";

    #[test]
    fn part1_works() {
        let result = process_part1(INPUT);
        assert_eq!(result, 15);
    }

    #[test]
    fn part2_works() {
        let result = process_part2(INPUT);
        assert_eq!(result, 12);
    }
}
