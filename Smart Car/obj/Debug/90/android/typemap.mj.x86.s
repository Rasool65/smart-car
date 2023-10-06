	/* Data SHA1: 25cf5054df2f8e7bde07fd38881485a17bf5bcf3 */
	.file	"typemap.mj.inc"

	/* Mapping header */
	.section	.data.mj_typemap,"aw",@progbits
	.type	mj_typemap_header, @object
	.p2align	2
	.global	mj_typemap_header
mj_typemap_header:
	/* version */
	.long	1
	/* entry-count */
	.long	3
	/* entry-length */
	.long	107
	/* value-offset */
	.long	46
	.size	mj_typemap_header, 16

	/* Mapping data */
	.type	mj_typemap, @object
	.global	mj_typemap
mj_typemap:
	.size	mj_typemap, 322
	.include	"typemap.mj.inc"
