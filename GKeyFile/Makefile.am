RAW_API=gkeyfile-api.raw
API=gkeyfile-api.xml
METADATA=GKeyFile.metadata

ASSEMBLY_NAME= gkeyfile-sharp
ASSEMBLY= $(ASSEMBLY_NAME).dll

CODEGEN=gapi2-codegen
FIXUP=gapi2-fixup

sources = 

build_sources = $(addprefix $(srcdir)/, $(sources)) AssemblyInfo.cs

customs = GKeyFile.custom

CLEANFILES = gkeyfile-sharp.dll generated-stamp generated/*.cs $(API) gkeyfile-sharp.snk

DISTCLEANFILES= AssemblyInfo.cs

noinst_DATA = gkeyfile-sharp.dll

moduledir = $(libdir)/mono/gkeyfile-sharp-1.0/
module_SCRIPTS = gkeyfile-sharp.dll.config

all: generated-stamp gkeyfile-sharp.dll

$(API): $(srcdir)/$(RAW_API) $(srcdir)/$(METADATA)
	cp $(srcdir)/$(RAW_API) $(API)
	chmod u+w $(API)
	$(FIXUP) --api=$(srcdir)/$(API) --metadata=$(srcdir)/$(METADATA)

generated-stamp: $(API)
	 $(CODEGEN) --generate $(srcdir)/$(API) $(GLIB_SHARP_CFLAGS) --outdir=generated --customdir=$(srcdir) --assembly-name=gkeyfile-sharp && touch generated-stamp

gkeyfile-sharp.snk: $(top_srcdir)/gkeyfile-sharp.snk
	cp $(top_srcdir)/gkeyfile-sharp.snk .

gkeyfile-sharp.dll: $(build_sources) generated-stamp gkeyfile-sharp.snk
	$(CSC) -out:gkeyfile-sharp.dll -unsafe -target:library $(GLIB_SHARP_LIBS) \
	$(build_sources) $(GENERATED_SOURCES)

install-data-local:
	echo "$(GACUTIL) /i $(ASSEMBLY) /f /package gkeyfile-sharp-1.0 /root $(DESTDIR)$(libdir)";  \
        $(GACUTIL) /i $(ASSEMBLY) /f /package gkeyfile-sharp-1.0 /root $(DESTDIR)$(libdir) || exit 1;

uninstall-local:
	echo "$(GACUTIL) /u $(ASSEMBLY_NAME) /package gkeyfile-sharp-2.0 /root $(DESTDIR)$(libdir)"; \
        $(GACUTIL) /u $(ASSEMBLY_NAME) /package gkeyfile-sharp-1.0 /root $(DESTDIR)$(libdir) || exit 1;

EXTRA_DIST = $(API) $(RAW_API) $(sources) $(customs) $(METADATA) gkeyfile-sharp.dll.config.in
