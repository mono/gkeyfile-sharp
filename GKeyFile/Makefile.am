RAW_API=gkeyfile-api.raw
API=gkeyfile-api.xml
METADATA=GKeyFile.metadata

ASSEMBLY_NAME= gkeyfile-sharp
ASSEMBLY= $(ASSEMBLY_NAME).dll

CODEGEN=gapi2-codegen
FIXUP=gapi2-fixup

GENERATED_SOURCEDIR = generated
GENERATED_SOURCES = $(GENERATED_SOURCEDIR)/*.cs

sources = AssemblyInfo.cs

build_sources = $(addprefix $(srcdir)/, $(sources))

customs = GKeyFile.custom

CLEANFILES = gkeyfile-sharp.dll $(GENERATED_SOURCES) $(API) gkeyfile-sharp.snk

DISTCLEANFILES= AssemblyInfo.cs

noinst_DATA = gkeyfile-sharp.dll

moduledir = $(libdir)/mono/gkeyfile-sharp-1.0/
module_SCRIPTS = gkeyfile-sharp.dll.config

all: gkeyfile-sharp.dll

$(API): $(srcdir)/$(RAW_API) $(srcdir)/$(METADATA)
	cp $(srcdir)/$(RAW_API) $(API)
	chmod u+w $(API)
	$(FIXUP) --api=$(API) --metadata=$(srcdir)/$(METADATA)

$(GENERATED_SOURCES): $(API)
	 $(CODEGEN) --generate $(API) $(GLIB_SHARP_CFLAGS) --outdir=$(GENERATED_SOURCEDIR) --customdir=$(srcdir) --assembly-name=gkeyfile-sharp

gkeyfile-sharp.dll: AssemblyInfo.cs $(GENERATED_SOURCES)
	$(CSC) -keyfile:$(top_srcdir)/gkeyfile-sharp.snk -out:$@ -unsafe -target:library $(GLIB_SHARP_LIBS) $^

install-data-local:
	echo "$(GACUTIL) /i $(ASSEMBLY) /f /package gkeyfile-sharp-1.0 /root $(DESTDIR)$(libdir)";  \
        $(GACUTIL) /i $(ASSEMBLY) /f /package gkeyfile-sharp-1.0 /root $(DESTDIR)$(libdir) || exit 1;

uninstall-local:
	echo "$(GACUTIL) /u $(ASSEMBLY_NAME) /package gkeyfile-sharp-2.0 /root $(DESTDIR)$(libdir)"; \
        $(GACUTIL) /u $(ASSEMBLY_NAME) /package gkeyfile-sharp-1.0 /root $(DESTDIR)$(libdir) || exit 1;

EXTRA_DIST = $(API) $(RAW_API) $(sources) $(customs) $(METADATA) gkeyfile-sharp.dll.config.in
