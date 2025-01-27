use std::collections::VecDeque;

#[derive(Debug, Clone, Copy)]
pub enum Spell {
  MagicMissile,
  Drain,
  Shield,
  Poison,
  Recharge,
}

#[derive(Debug, Clone, Copy)]
pub struct Battle {
  pub player_hp: i32,
  pub player_mana: i32,
  pub player_armor: i32,
  pub player_mana_spent: i32,

  pub boss_hp: i32,
  pub boss_damage: i32,

  pub shield_timer: i32,
  pub poison_timer: i32,
  pub recharge_timer: i32,
}

#[derive(Debug, Clone, Copy)]
pub struct Boss {
  pub hit_points: i32,
  pub damage: i32,
}

#[derive(Debug, Clone, Copy)]
pub struct BattleResult {
  pub victory: bool,
  pub mana_spent: i32,
}

static MAGIC_MISSILE_COST: i32 = 53;
static DRAIN_COST: i32 = 73;
static SHIELD_COST: i32 = 113;
static POISON_COST: i32 = 173;
static RECHARGE_COST: i32 = 229;

static MAGIC_MISSILE_DAMAGE: i32 = 4;
static DRAIN_DAMAGE: i32 = 2;

static SHIELD_ARMOR: i32 = 7;
static POISON_DAMAGE: i32 = 3;
static RECHARGE_MANA: i32 = 101;

pub fn process_battle(battle: Battle) -> BattleResult {
  let mut min_mana = i32::MAX;
  let mut queue = VecDeque::new();

  for spell in &[
    Spell::MagicMissile,
    Spell::Drain,
    Spell::Shield,
    Spell::Poison,
    Spell::Recharge,
  ] {
    queue.push_back((battle, spell));
  }

  while let Some((mut battle, next_spell)) = queue.pop_front() {
    apply_effects(&mut battle);

    if battle.boss_hp <= 0 {
      min_mana = min_mana.min(battle.player_mana_spent);
      continue;
    }

    proces_spell(&mut battle, next_spell);

    if battle.player_mana_spent > min_mana {
      continue;
    }

    if battle.boss_hp <= 0 {
      min_mana = min_mana.min(battle.player_mana_spent);
      continue;
    }

    apply_effects(&mut battle);

    if battle.boss_hp <= 0 {
      min_mana = min_mana.min(battle.player_mana_spent);
      continue;
    }

    apply_boss_damage(&mut battle);

    if battle.player_hp <= 0 {
      continue;
    }

    queue_spells(&mut queue, battle);
  }

  BattleResult {
    victory: min_mana != i32::MAX,
    mana_spent: min_mana,
  }
}

fn apply_effects(battle: &mut Battle) {
  battle.player_armor = 0;

  if battle.shield_timer > 0 {
    battle.shield_timer -= 1;
    battle.player_armor = SHIELD_ARMOR;
  }

  if battle.poison_timer > 0 {
    battle.poison_timer -= 1;
    battle.boss_hp -= POISON_DAMAGE;
  }

  if battle.recharge_timer > 0 {
    battle.recharge_timer -= 1;
    battle.player_mana += RECHARGE_MANA;
  }
}

fn proces_spell(battle: &mut Battle, next_spell: &Spell) {
  let mana_cost;
  match next_spell {
    Spell::MagicMissile => {
      mana_cost = MAGIC_MISSILE_COST;
      battle.boss_hp -= MAGIC_MISSILE_DAMAGE;
    }
    Spell::Drain => {
      mana_cost = DRAIN_COST;
      battle.boss_hp -= DRAIN_DAMAGE;
      battle.player_hp += DRAIN_DAMAGE;
    }
    Spell::Shield => {
      mana_cost = SHIELD_COST;
      battle.shield_timer = 6;
    }
    Spell::Poison => {
      mana_cost = POISON_COST;
      battle.poison_timer = 6;
    }
    Spell::Recharge => {
      mana_cost = RECHARGE_COST;
      battle.recharge_timer = 5;
    }
  }

  battle.player_mana -= mana_cost;
  battle.player_mana_spent += mana_cost;
}

fn apply_boss_damage(battle: &mut Battle) {
  if battle.boss_damage > battle.player_armor {
    battle.player_hp -= battle.boss_damage - battle.player_armor;
  } else {
    battle.player_hp -= 1;
  }
}

fn queue_spells(queue: &mut VecDeque<(Battle, &Spell)>, battle: Battle) {
    if battle.player_mana >= MAGIC_MISSILE_COST {
      queue.push_back((battle, &Spell::MagicMissile));
    }
    if battle.player_mana >= DRAIN_COST {
      queue.push_back((battle, &Spell::Drain));
    }
    if battle.player_mana >= SHIELD_COST && battle.shield_timer <= 1 {
      queue.push_back((battle, &Spell::Shield));
    }
    if battle.player_mana >= POISON_COST && battle.poison_timer <= 1 {
      queue.push_back((battle, &Spell::Poison));
    }
    if battle.player_mana >= RECHARGE_COST && battle.recharge_timer <= 1 {
      queue.push_back((battle, &Spell::Recharge));
    }
}
