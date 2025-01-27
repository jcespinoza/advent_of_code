use itertools::Itertools;

pub fn get_lowest_entanglement(package_weights: Vec<i32>) -> i64 {
  let total_weigth: i32 = package_weights.iter().sum();
  let target_weight = total_weigth / 3;

  let mut min_package_count = 1;
  let mut combo_with_lowest_entanglement: Option<i64>;

  loop {
    combo_with_lowest_entanglement = package_weights
      .iter()
      .copied()
      .combinations(min_package_count)
      .filter(|combination| combination.iter().sum::<i32>() == target_weight)
      .map(|combo| combo.iter().map(|x| *x as i64).product::<i64>())
      .min();

    if combo_with_lowest_entanglement.is_some() {
      break;
    }

    min_package_count += 1;
  }

  combo_with_lowest_entanglement.unwrap()
}
