using System;
using System.Collections.Generic;
using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data;

public partial class TestdbContext : DbContext
{
    public TestdbContext()
    {
    }

    public TestdbContext(DbContextOptions<TestdbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Banner> Banners { get; set; }

    public virtual DbSet<BannerDetail> BannerDetails { get; set; }

    public virtual DbSet<Brand> Brands { get; set; }

    public virtual DbSet<Collection> Collections { get; set; }

    public virtual DbSet<ImageStorage> ImageStorages { get; set; }

    public virtual DbSet<Item> Items { get; set; }

    public virtual DbSet<LifeStyle> LifeStyles { get; set; }

    public virtual DbSet<Material> Materials { get; set; }

    public virtual DbSet<MaterialDetail> MaterialDetails { get; set; }

    public virtual DbSet<ProductType> ProductTypes { get; set; }

    public virtual DbSet<Room> Rooms { get; set; }

    public virtual DbSet<Style> Styles { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Banner>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("banners_pkey");

            entity.ToTable("banners");

            entity.HasIndex(e => e.BannerName, "banners_banner_name_idx");

            entity.HasIndex(e => e.Id, "banners_id_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BannerName)
                .HasMaxLength(128)
                .HasColumnName("banner_name");
            entity.Property(e => e.Description)
                .HasMaxLength(1024)
                .HasColumnName("description");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("true")
                .HasColumnName("is_active");
            entity.Property(e => e.SortOrder)
                .ValueGeneratedOnAdd()
                .HasColumnName("sort_order");
        });

        modelBuilder.Entity<BannerDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("banner_details_pkey");

            entity.ToTable("banner_details");

            entity.HasIndex(e => new { e.Id, e.BannerId }, "banner_details_id_banner_id_idx");

            entity.HasIndex(e => e.UrlCode, "banner_details_url_code_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BannerDetailName)
                .HasMaxLength(256)
                .HasColumnName("banner_detail_name");
            entity.Property(e => e.BannerId)
                .ValueGeneratedOnAdd()
                .HasColumnName("banner_id");
            entity.Property(e => e.Description)
                .HasMaxLength(2048)
                .HasColumnName("description");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(256)
                .HasColumnName("display_name");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(2048)
                .HasColumnName("image_url");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("true")
                .HasColumnName("is_active");
            entity.Property(e => e.QuickTitle)
                .HasMaxLength(2048)
                .HasColumnName("quick_title");
            entity.Property(e => e.SortOrder).HasColumnName("sort_order");
            entity.Property(e => e.UrlCode)
                .HasMaxLength(256)
                .HasColumnName("url_code");

            entity.HasOne(d => d.Banner).WithMany(p => p.BannerDetails)
                .HasForeignKey(d => d.BannerId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("banner_details_banner_id_fkey");
        });

        modelBuilder.Entity<Brand>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("brands_pkey");

            entity.ToTable("brands");

            entity.HasIndex(e => new { e.BrandName, e.UrlCode }, "brands_brand_name_url_code_idx");

            entity.HasIndex(e => new { e.BrandName, e.UrlCode }, "brands_brand_name_url_code_idx1");

            entity.HasIndex(e => e.Id, "brands_id_idx");

            entity.HasIndex(e => e.Id, "brands_id_idx1");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BrandName)
                .HasMaxLength(256)
                .HasColumnName("brand_name");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.Description)
                .HasMaxLength(2048)
                .HasColumnName("description");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(256)
                .HasColumnName("display_name");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(2048)
                .HasColumnName("image_url");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("true")
                .HasColumnName("is_active");
            entity.Property(e => e.IsDescription)
                .HasDefaultValueSql("false")
                .HasColumnName("is_description");
            entity.Property(e => e.IsDisplayName)
                .HasDefaultValueSql("false")
                .HasColumnName("is_display_name");
            entity.Property(e => e.IsImage)
                .HasDefaultValueSql("false")
                .HasColumnName("is_image");
            entity.Property(e => e.IsLogo)
                .HasDefaultValueSql("false")
                .HasColumnName("is_logo");
            entity.Property(e => e.MetaDescription)
                .HasMaxLength(2048)
                .HasColumnName("meta_description");
            entity.Property(e => e.MetaKeyword)
                .HasMaxLength(2048)
                .HasColumnName("meta_keyword");
            entity.Property(e => e.SortOrder).HasColumnName("sort_order");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_on");
            entity.Property(e => e.UrlCode)
                .HasMaxLength(256)
                .HasColumnName("url_code");
        });

        modelBuilder.Entity<Collection>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("collections_pkey");

            entity.ToTable("collections");

            entity.HasIndex(e => new { e.BrandId, e.CollectionName, e.UrlCode, e.DisplayName }, "collections_brand_id_collection_name_url_code_display_name_idx");

            entity.HasIndex(e => e.Id, "collections_id_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.BrandId)
                .ValueGeneratedOnAdd()
                .HasColumnName("brand_id");
            entity.Property(e => e.CollectionName)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnName("collection_name");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.Desription)
                .HasMaxLength(2048)
                .HasColumnName("desription");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(256)
                .HasColumnName("display_name");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(2048)
                .HasColumnName("image_url");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("true")
                .HasColumnName("is_active");
            entity.Property(e => e.IsDescription)
                .HasDefaultValueSql("false")
                .HasColumnName("is_description");
            entity.Property(e => e.IsDisplayName)
                .HasDefaultValueSql("false")
                .HasColumnName("is_display_name");
            entity.Property(e => e.IsImage)
                .HasDefaultValueSql("false")
                .HasColumnName("is_image");
            entity.Property(e => e.IsLogo)
                .HasDefaultValueSql("false")
                .HasColumnName("is_logo");
            entity.Property(e => e.IsStory)
                .HasDefaultValueSql("false")
                .HasColumnName("is_story");
            entity.Property(e => e.MetaDescription)
                .HasMaxLength(2048)
                .HasColumnName("meta_description");
            entity.Property(e => e.MetaKeyword)
                .HasMaxLength(2048)
                .HasColumnName("meta_keyword");
            entity.Property(e => e.SortOrder).HasColumnName("sort_order");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_on");
            entity.Property(e => e.UrlCode)
                .HasMaxLength(256)
                .HasColumnName("url_code");

            entity.HasOne(d => d.Brand).WithMany(p => p.Collections)
                .HasForeignKey(d => d.BrandId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("collections_brand_id_fkey");
        });

        modelBuilder.Entity<ImageStorage>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("image_storages_pkey");

            entity.ToTable("image_storages");

            entity.HasIndex(e => e.Id, "image_storages_id_idx");

            entity.HasIndex(e => e.ItemId, "image_storages_item_id_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(2048)
                .HasColumnName("image_url");
            entity.Property(e => e.ItemId)
                .ValueGeneratedOnAdd()
                .HasColumnName("item_id");
            entity.Property(e => e.SortOrder).HasColumnName("sort_order");
        });

        modelBuilder.Entity<Item>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("items_pkey");

            entity.ToTable("items");

            entity.HasIndex(e => e.Id, "items_id_idx");

            entity.HasIndex(e => new { e.Sku, e.ProductName, e.UrlCode, e.IsActive }, "items_sku_product_name_url_code_is_active_idx");

            entity.HasIndex(e => new { e.TypeId, e.CollectionId, e.RoomId, e.StyleId, e.LifeStyleId }, "items_type_id_collection_id_room_id_style_id_life_style_id_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.AdditionalFeatures)
                .HasMaxLength(2048)
                .HasColumnName("additional_features");
            entity.Property(e => e.Cbm).HasColumnName("cbm");
            entity.Property(e => e.ChairArmHeight).HasColumnName("chair_arm_height");
            entity.Property(e => e.ChairInsideSeatDepth).HasColumnName("chair_inside_seat_depth");
            entity.Property(e => e.ChairInsideSeatWidth).HasColumnName("chair_inside_seat_width");
            entity.Property(e => e.ChairSeatHeight).HasColumnName("chair_seat_height");
            entity.Property(e => e.CmSideAndFrontRailApronClearance).HasColumnName("cm_sideAndFrontRailApronClearance");
            entity.Property(e => e.CmSlatToTopOfFootRailClearance).HasColumnName("cm_slatToTopOfFootRailClearance");
            entity.Property(e => e.CmSlatToTopOfSideRailClearance).HasColumnName("cm_slatToTopOfSideRailClearance");
            entity.Property(e => e.CollectionId)
                .ValueGeneratedOnAdd()
                .HasColumnName("collection_id");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.DefaultCode)
                .HasMaxLength(64)
                .HasColumnName("default_code");
            entity.Property(e => e.Depth).HasColumnName("depth");
            entity.Property(e => e.Description)
                .HasMaxLength(2048)
                .HasColumnName("description");
            entity.Property(e => e.ExtenedDesctiption)
                .HasMaxLength(2048)
                .HasColumnName("extened_desctiption");
            entity.Property(e => e.FinishName)
                .HasMaxLength(2048)
                .HasColumnName("finish_name");
            entity.Property(e => e.GrossWeightInch).HasColumnName("gross_weight_inch");
            entity.Property(e => e.GrossWeightKg).HasColumnName("gross_weight_kg");
            entity.Property(e => e.Height).HasColumnName("height");
            entity.Property(e => e.ImageMainUrl)
                .HasMaxLength(128)
                .HasColumnName("image_main_url");
            entity.Property(e => e.InSideAndFrontRailApronClearance).HasColumnName("in_sideAndFrontRailApronClearance");
            entity.Property(e => e.InSlatToTopOfFootRailClearance).HasColumnName("in_slatToTopOfFootRailClearance");
            entity.Property(e => e.InSlatToTopOfSideRailClearance).HasColumnName("in_slatToTopOfSideRailClearance");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("true")
                .HasColumnName("is_active");
            entity.Property(e => e.IsBestSeller)
                .HasDefaultValueSql("false")
                .HasColumnName("is_best_seller");
            entity.Property(e => e.IsNew)
                .HasDefaultValueSql("false")
                .HasColumnName("is_new");
            entity.Property(e => e.LifeStyleId)
                .ValueGeneratedOnAdd()
                .HasColumnName("life_style_id");
            entity.Property(e => e.MetaDescription)
                .HasMaxLength(2048)
                .HasColumnName("meta_description");
            entity.Property(e => e.MetaKeyword)
                .HasMaxLength(2048)
                .HasColumnName("meta_keyword");
            entity.Property(e => e.NetWeightInch).HasColumnName("net_weight_inch");
            entity.Property(e => e.NetWeightKg).HasColumnName("net_weight_kg");
            entity.Property(e => e.ParentCode)
                .HasMaxLength(64)
                .HasColumnName("parent_code");
            entity.Property(e => e.Price).HasColumnName("price");
            entity.Property(e => e.PrimaryMaterialId)
                .ValueGeneratedOnAdd()
                .HasColumnName("primary_material_id");
            entity.Property(e => e.ProductName)
                .HasMaxLength(256)
                .HasColumnName("product_name");
            entity.Property(e => e.RoomId)
                .ValueGeneratedOnAdd()
                .HasColumnName("room_id");
            entity.Property(e => e.SecondaryMaterialId)
                .ValueGeneratedOnAdd()
                .HasColumnName("secondary_material_id");
            entity.Property(e => e.Sku)
                .IsRequired()
                .HasMaxLength(64)
                .HasColumnName("sku");
            entity.Property(e => e.StyleId)
                .ValueGeneratedOnAdd()
                .HasColumnName("style_id");
            entity.Property(e => e.TableClearance).HasColumnName("table_clearance");
            entity.Property(e => e.TableCloseDepth).HasColumnName("table_close_depth");
            entity.Property(e => e.TableCloseHeight).HasColumnName("table_close_height");
            entity.Property(e => e.TableCloseWidth).HasColumnName("table_close_width");
            entity.Property(e => e.TableLeavesCount).HasColumnName("table_leaves_count");
            entity.Property(e => e.TableLeavesWidth).HasColumnName("table_leaves_width");
            entity.Property(e => e.TableSeatsCountClosed).HasColumnName("table_seats_count_closed");
            entity.Property(e => e.TableSeatsCountOpen).HasColumnName("table_seats_count_open");
            entity.Property(e => e.TypeId)
                .ValueGeneratedOnAdd()
                .HasColumnName("type_id");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_on");
            entity.Property(e => e.UrlCode)
                .HasMaxLength(256)
                .HasColumnName("url_code");
            entity.Property(e => e.VariationDescription)
                .HasMaxLength(2048)
                .HasColumnName("variation_description");
            entity.Property(e => e.Width).HasColumnName("width");

            entity.HasOne(d => d.Collection).WithMany(p => p.Items)
                .HasForeignKey(d => d.CollectionId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("items_collection_id_fkey");

            entity.HasOne(d => d.LifeStyle).WithMany(p => p.Items)
                .HasForeignKey(d => d.LifeStyleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("items_life_style_id_fkey");

            entity.HasOne(d => d.Room).WithMany(p => p.Items)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("items_room_id_fkey");

            entity.HasOne(d => d.Style).WithMany(p => p.Items)
                .HasForeignKey(d => d.StyleId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("items_style_id_fkey");

            entity.HasOne(d => d.Type).WithMany(p => p.Items)
                .HasForeignKey(d => d.TypeId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("items_type_id_fkey");
        });

        modelBuilder.Entity<LifeStyle>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("life_styles_pkey");

            entity.ToTable("life_styles");

            entity.HasIndex(e => e.Id, "life_styles_id_idx");

            entity.HasIndex(e => new { e.LifeStyleName, e.UrlCode, e.DisplayName }, "life_styles_life_style_name_url_code_display_name_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.Desription)
                .HasMaxLength(2048)
                .HasColumnName("desription");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(256)
                .HasColumnName("display_name");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(2048)
                .HasColumnName("image_url");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("true")
                .HasColumnName("is_active");
            entity.Property(e => e.IsDescription)
                .HasDefaultValueSql("false")
                .HasColumnName("is_description");
            entity.Property(e => e.IsDisplayName)
                .HasDefaultValueSql("false")
                .HasColumnName("is_display_name");
            entity.Property(e => e.IsImage)
                .HasDefaultValueSql("false")
                .HasColumnName("is_image");
            entity.Property(e => e.IsLogo)
                .HasDefaultValueSql("false")
                .HasColumnName("is_logo");
            entity.Property(e => e.IsStory)
                .HasDefaultValueSql("false")
                .HasColumnName("is_story");
            entity.Property(e => e.LifeStyleName)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnName("life_style_name");
            entity.Property(e => e.MetaDescription)
                .HasMaxLength(2048)
                .HasColumnName("meta_description");
            entity.Property(e => e.MetaKeyword)
                .HasMaxLength(2048)
                .HasColumnName("meta_keyword");
            entity.Property(e => e.SortOrder).HasColumnName("sort_order");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_on");
            entity.Property(e => e.UrlCode)
                .HasMaxLength(256)
                .HasColumnName("url_code");
        });

        modelBuilder.Entity<Material>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("materials_pkey");

            entity.ToTable("materials");

            entity.HasIndex(e => e.Id, "materials_id_idx");

            entity.HasIndex(e => e.MaterialName, "materials_material_name_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Desription)
                .HasMaxLength(2048)
                .HasColumnName("desription");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(256)
                .HasColumnName("display_name");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(2048)
                .HasColumnName("image_url");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("true")
                .HasColumnName("is_active");
            entity.Property(e => e.MaterialName)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnName("material_name");
            entity.Property(e => e.MetaDescription)
                .HasMaxLength(2048)
                .HasColumnName("meta_description");
            entity.Property(e => e.MetaKeyword)
                .HasMaxLength(2048)
                .HasColumnName("meta_keyword");
            entity.Property(e => e.SortOrder).HasColumnName("sort_order");
            entity.Property(e => e.UrlCode)
                .HasMaxLength(256)
                .HasColumnName("url_code");
        });

        modelBuilder.Entity<MaterialDetail>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("material_details_pkey");

            entity.ToTable("material_details");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Desription)
                .HasMaxLength(2048)
                .HasColumnName("desription");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(128)
                .HasColumnName("display_name");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(2048)
                .HasColumnName("image_url");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("true")
                .HasColumnName("is_active");
            entity.Property(e => e.MaterialDetailName)
                .HasMaxLength(128)
                .HasColumnName("material_detail_name");
            entity.Property(e => e.MaterialId)
                .ValueGeneratedOnAdd()
                .HasColumnName("material_id");
            entity.Property(e => e.MetaDescription)
                .HasMaxLength(2048)
                .HasColumnName("meta_description");
            entity.Property(e => e.MetaKeyword)
                .HasMaxLength(2048)
                .HasColumnName("meta_keyword");

            entity.HasOne(d => d.Material).WithMany(p => p.MaterialDetails)
                .HasForeignKey(d => d.MaterialId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("material_details_material_id_fkey");
        });

        modelBuilder.Entity<ProductType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("types_pkey");

            entity.ToTable("product_types");

            entity.HasIndex(e => e.Id, "types_id_idx");

            entity.HasIndex(e => new { e.TypeName, e.UrlCode, e.DisplayName }, "types_type_name_url_code_display_name_idx");

            entity.Property(e => e.Id)
                .HasDefaultValueSql("nextval('types_id_seq'::regclass)")
                .HasColumnName("id");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.Desription)
                .HasMaxLength(2048)
                .HasColumnName("desription");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(256)
                .HasColumnName("display_name");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(2048)
                .HasColumnName("image_url");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("true")
                .HasColumnName("is_active");
            entity.Property(e => e.IsDescription)
                .HasDefaultValueSql("false")
                .HasColumnName("is_description");
            entity.Property(e => e.IsDisplayName)
                .HasDefaultValueSql("false")
                .HasColumnName("is_display_name");
            entity.Property(e => e.IsImage)
                .HasDefaultValueSql("false")
                .HasColumnName("is_image");
            entity.Property(e => e.IsLogo)
                .HasDefaultValueSql("false")
                .HasColumnName("is_logo");
            entity.Property(e => e.IsStory)
                .HasDefaultValueSql("false")
                .HasColumnName("is_story");
            entity.Property(e => e.MetaDescription)
                .HasMaxLength(2048)
                .HasColumnName("meta_description");
            entity.Property(e => e.MetaKeyword)
                .HasMaxLength(2048)
                .HasColumnName("meta_keyword");
            entity.Property(e => e.RoomId)
                .HasDefaultValueSql("nextval('types_room_id_seq'::regclass)")
                .HasColumnName("room_id");
            entity.Property(e => e.SortOrder).HasColumnName("sort_order");
            entity.Property(e => e.TypeName)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnName("type_name");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_on");
            entity.Property(e => e.UrlCode)
                .HasMaxLength(256)
                .HasColumnName("url_code");

            entity.HasOne(d => d.Room).WithMany(p => p.ProductTypes)
                .HasForeignKey(d => d.RoomId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("types_room_id_fkey");
        });

        modelBuilder.Entity<Room>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("rooms_pkey");

            entity.ToTable("rooms");

            entity.HasIndex(e => e.Id, "rooms_id_idx");

            entity.HasIndex(e => new { e.RoomName, e.UrlCode, e.DisplayName }, "rooms_room_name_url_code_display_name_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.Desription)
                .HasMaxLength(2048)
                .HasColumnName("desription");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(256)
                .HasColumnName("display_name");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(2048)
                .HasColumnName("image_url");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("true")
                .HasColumnName("is_active");
            entity.Property(e => e.IsDescription)
                .HasDefaultValueSql("false")
                .HasColumnName("is_description");
            entity.Property(e => e.IsDisplayName)
                .HasDefaultValueSql("false")
                .HasColumnName("is_display_name");
            entity.Property(e => e.IsImage)
                .HasDefaultValueSql("false")
                .HasColumnName("is_image");
            entity.Property(e => e.IsLogo)
                .HasDefaultValueSql("false")
                .HasColumnName("is_logo");
            entity.Property(e => e.IsStory)
                .HasDefaultValueSql("false")
                .HasColumnName("is_story");
            entity.Property(e => e.MetaDescription)
                .HasMaxLength(2048)
                .HasColumnName("meta_description");
            entity.Property(e => e.MetaKeyword)
                .HasMaxLength(2048)
                .HasColumnName("meta_keyword");
            entity.Property(e => e.RoomName)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnName("room_name");
            entity.Property(e => e.SortOrder).HasColumnName("sort_order");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_on");
            entity.Property(e => e.UrlCode)
                .HasMaxLength(256)
                .HasColumnName("url_code");
        });

        modelBuilder.Entity<Style>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("styles_pkey");

            entity.ToTable("styles");

            entity.HasIndex(e => e.Id, "styles_id_idx");

            entity.HasIndex(e => new { e.StyleName, e.UrlCode, e.DisplayName }, "styles_style_name_url_code_display_name_idx");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreatedBy)
                .HasMaxLength(100)
                .HasColumnName("created_by");
            entity.Property(e => e.CreatedOn)
                .HasDefaultValueSql("now()")
                .HasColumnType("timestamp without time zone")
                .HasColumnName("created_on");
            entity.Property(e => e.Desription)
                .HasMaxLength(2048)
                .HasColumnName("desription");
            entity.Property(e => e.DisplayName)
                .HasMaxLength(256)
                .HasColumnName("display_name");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(2048)
                .HasColumnName("image_url");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("true")
                .HasColumnName("is_active");
            entity.Property(e => e.IsDescription)
                .HasDefaultValueSql("false")
                .HasColumnName("is_description");
            entity.Property(e => e.IsDisplayName)
                .HasDefaultValueSql("false")
                .HasColumnName("is_display_name");
            entity.Property(e => e.IsImage)
                .HasDefaultValueSql("false")
                .HasColumnName("is_image");
            entity.Property(e => e.IsLogo)
                .HasDefaultValueSql("false")
                .HasColumnName("is_logo");
            entity.Property(e => e.IsStory)
                .HasDefaultValueSql("false")
                .HasColumnName("is_story");
            entity.Property(e => e.MetaDescription)
                .HasMaxLength(2048)
                .HasColumnName("meta_description");
            entity.Property(e => e.MetaKeyword)
                .HasMaxLength(2048)
                .HasColumnName("meta_keyword");
            entity.Property(e => e.SortOrder).HasColumnName("sort_order");
            entity.Property(e => e.StyleName)
                .IsRequired()
                .HasMaxLength(256)
                .HasColumnName("style_name");
            entity.Property(e => e.UpdatedBy)
                .HasMaxLength(100)
                .HasColumnName("updated_by");
            entity.Property(e => e.UpdatedOn)
                .HasColumnType("timestamp without time zone")
                .HasColumnName("updated_on");
            entity.Property(e => e.UrlCode)
                .HasMaxLength(256)
                .HasColumnName("url_code");
        });
        base.OnModelCreating(modelBuilder);
    }
}
