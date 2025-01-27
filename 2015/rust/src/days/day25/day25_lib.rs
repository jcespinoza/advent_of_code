use crate::days::day03::Point;

pub fn compute_code_at(target_location: Point) -> i64 {
  let ordinal = get_ordinal(target_location);

  let mut code = 20151125;
  for _ in 1..ordinal {
    code = (code * 252533) % 33554393;
  }

  code
}

fn get_ordinal(target_location: Point) -> i64 {
  let t_row = target_location.row as i64;
  let t_col = target_location.col as i64;
  let triangle_base = t_row + t_col - 1;

  let count = (triangle_base * (triangle_base + 1)) / 2;

  let ordinal = (count - t_row) + 1;

  ordinal
}
