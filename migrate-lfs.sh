#!/bin/bash

echo "🚨 Git LFS – Migration complète de l’historique (CAS 1)"
echo "------------------------------------------------------"
echo "⚠️  Ce script RÉÉCRIT L'HISTORIQUE Git."
echo "⚠️  Tous les collaborateurs devront faire un reset --hard."
echo ""

read -p "👉 Continuer ? (yes pour continuer) : " CONFIRM
if [ "$CONFIRM" != "yes" ]; then
  echo "❌ Opération annulée."
  exit 1
fi

# =========================
# PRÉCONDITIONS
# =========================

if ! git rev-parse --is-inside-work-tree &> /dev/null; then
  echo "❌ Ce script doit être lancé depuis un dépôt Git."
  exit 1
fi

if ! command -v git-lfs &> /dev/null; then
  echo "❌ Git LFS n'est pas installé. Lance d'abord le script d'installation."
  exit 1
fi

# =========================
# CONFIG
# =========================

LFS_PATTERNS="*.psd,*.png,*.jpg,*.tga,*.exr,*.wav,*.mp3,*.mp4,*.mov,*.fbx,*.blend,*.spp"

# =========================
# SAUVEGARDE SÉCURITÉ
# =========================

echo "📦 Sauvegarde de sécurité (tag pre-lfs-migration)..."
git tag pre-lfs-migration

# =========================
# MIGRATION HISTORIQUE
# =========================

echo "🧹 Migration complète de l'historique vers Git LFS..."
git lfs migrate import --everything --include="$LFS_PATTERNS"

if [ $? -ne 0 ]; then
  echo "❌ Échec de la migration Git LFS."
  exit 1
fi

# =========================
# NETTOYAGE LOCAL
# =========================

echo "🧽 Nettoyage du repo local..."
git reflog expire --expire=now --all
git gc --prune=now --aggressive

# =========================
# PUSH FORCÉ
# =========================

echo "🚀 Push forcé vers le remote..."
git push --force --all
git push --force --tags

# =========================
# FIN
# =========================

echo ""
echo "✅ Migration terminée avec succès."
echo ""
echo "📢 À communiquer aux collaborateurs :"
echo "-----------------------------------"
echo "git fetch"
echo "git reset --hard origin/main"
echo ""
echo "🎉 Repo propre, Git LFS actif sur TOUT l’historique."
